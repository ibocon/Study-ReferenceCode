###########################################################
#		Program Description
#  CS315 Lab 10 -  Multidimensional arrays
#
#  1. Use create_col_matrix to create and read a column
#     major matrix
#  2. Use print_col_matrix to print the matrix
#  3. Read an element index (row, column) from the user
#  4. Use print_element to print the element from the matrix
#
#  Reminder:
#   There are multiple D2L dropbox pages
#   If dropboxes are not visible for later labs, check
#   the bottom of the page for the 'next page' arrow
#   to find them.

###########################################################
#		Register Usage
#	$t0	BaseAddress
#	$t1	Hight
#	$t2	Width
#	$t3	
#	$t4	
#	$t5	
#	$t6
#	$t7
#	$t8
#	$t9
###########################################################
		.data
base:	.word 0
hight:	.word 0
width:	.word 0
###########################################################
		.text
main:
	# Reminder:  Stack MUST be used in main!

#1 Create matrix and Read
	addiu $sp,$sp,-16
	sw $ra,12($sp)	#save R main

	jal create_col_matrix

	lw $t0,0($sp)	#t0=Base
	lw $t1,4($sp)	#t1=Hight
	lw $t2,8($sp)	#t2=Width
	lw $ra,12($sp)	#ra=Main
	addiu $sp,$sp,16

	la $t9,base
	sw $t0,0($t9)	#backup Base
	la $t9,hight
	sw $t1,0($t9)	#backup Hight
	la $t9,width
	sw $t2,0($t9)	#backup Width

#2 Print matrix	
	addiu $sp,$sp,-16
	sw $t0,0($sp)	#IN Base
	sw $t1,4($sp)	#IN Hight
	sw $t2,8($sp)	#IN Width
	sw $ra,12($sp)	#R Main

	jal print_col_matrix

	lw $ra,12($sp)
	addiu $sp,$sp,16

#3 Find element from matrix
	la $t9,base
	lw $t0,0($t9)	#load Base
	la $t9,hight
	lw $t1,0($t9)	#load Hight
	la $t9,width
	lw $t2,0($t9)	#load Width

	addiu $sp,$sp,-16
	sw $t0,0($sp)	#IN base
	sw $t1,4($sp)	#IN hight
	sw $t2,8($sp)	#IN Width
	sw $ra,12($sp)	#R Main

	jal print_element

	lw $ra,12($sp)
	addiu $sp,$sp,16
	
mainEnd:
	li $v0, 10
	syscall				# Halt
###########################################################


###########################################################
#		Subprogram Description
#  Ask the user for a height and width, dynamically alocate
#   a matrix of words with the given dimensions
#  Call read_col_matrix to fill the matrix

###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp+0  Base address (OUT)
#	$sp+4  Height (OUT)
#	$sp+8  Width (OUT)
#	$sp+12
###########################################################
#		Register Usage
#	$t0  Base address
#	$t1  Height
#	$t2  Width
#	$t3  Size
#	$t4
#	$t5
#	$t6
#	$t7
#	$t8
#	$t9
###########################################################
		.data
create_msg:	.asciiz"\nCreating..\n"
height_msg:	.asciiz"Enter height: "
width_msg:	.asciiz"Enter width: "
createError_p:	.asciiz"\nInvalid value!\n"
###########################################################
		.text
create_col_matrix:
	li $v0,4
	la $a0,create_msg
	syscall
#GET Height
	li $v0,4
	la $a0,height_msg
	syscall

	li $v0,5
	syscall
	move $t1,$v0

	blez $t1,createError
	sw $t1,4($sp)	#Store Hight
	
#GET Width
	li $v0,4
	la $a0,width_msg
	syscall

	li $v0,5
	syscall
	move $t2,$v0
	
	blez $t2,createError
	sw $t2,8($sp)	#Store Width
	
	b creating
createError:
	li $v0,4
	la $a0,createError_p
	syscall
	b create_col_matrix
	
creating:
	mul $t3,$t1,$t2
	sll $a0,$t3,2
	li $v0,9
	syscall
	
	move $t0,$v0
	sw $t0,0($sp)	#sp+0 = baseAddress

#Start Read
	addiu $sp,$sp,-12
	sw $t0,0($sp)	#IN Base
	sw $t3,4($sp)	#IN Size
	sw $ra,8($sp)	#R Create

	jal read_col_matrix

	lw $ra,8($sp)
	addiu $sp,$sp,12
	
	jr $ra
###########################################################


###########################################################
#		Subprogram Description
#  Reads words into an the matrix until the matrix is full
#  Elements will be stored in column-major order

###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp+0  Base address (IN)
#	$sp+4  Size (IN)
#	$sp+8  
#	$sp+12
###########################################################
#		Register Usage
#	$t0	Base
#	$t1	Size
#	$t2	
#	$t3	
#	$t4	
#	$t5	
#	$t6
#	$t7
#	$t8
#	$t9
###########################################################
		.data
read_msg:	.asciiz"\nReading..\n"
enter:		.asciiz"Enter an integer: "
###########################################################
		.text
read_col_matrix:
	lw $t0,0($sp)
	lw $t1,4($sp)

	li $v0,4
	la $a0,read_msg
	syscall
	
readFor:
	blez $t1,readEnd
	
	li $v0,4
	la $a0,enter
	syscall

	li $v0,5
	syscall

	sw $v0,0($t0)

	addi $t0,$t0,4
	addi $t1,$t1,-1
	
	b readFor
readEnd:
	jr $ra
###########################################################


###########################################################
#		Subprogram Description
#  Prints a column-major matrix

###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp+0  Base address (IN)
#	$sp+4  Hight (IN)
#	$sp+8  Width (IN)
#	$sp+12
###########################################################
#		Register Usage
#	$t0 Base
#	$t1 Height
#	$t2 Width
#	$t3 Row
#	$t4 Column
#	$t5 Index
#	$t6
#	$t7
#	$t8
#	$t9
###########################################################
		.data
print_msg:	.asciiz"\nPrinting..\n"
printelement:	.asciiz"<Matrix>\n"
print_enter:	.asciiz"new C:"
print_space:	.asciiz"  "
###########################################################
		.text
print_col_matrix:
	lw $t0,0($sp)
	lw $t1,4($sp)
	lw $t2,8($sp)
	
	li $t5,0
	
	li $v0,4
	la $a0,print_msg
	syscall
	
	li $v0,4
	la $a0,printelement
	syscall

	li $t4,0	#Column=0
	
	b printFor
printFor:
	bge $t4,$t2,printEnd #Width>Column
	li $v0,4
	la $a0,print_enter
	syscall
	li $t3,0	#Row=0
	b print2For
print2For:
	bge $t3,$t1,printFor2 #Height>Row
	
	mul $t5,$t1,$t4	#Index=Height*Column
	add $t5,$t5,$t3	#Index+=Row
	sll $t5,$t5,2	#Index*=Element's Size
	add $t5,$t5,$t0	#Index+=Base
	
	li $v0,1
	lw $a0,0($t5)
	syscall
	
	li $v0,4
	la $a0,print_space
	syscall
	
	addiu $t3,$t3,1	#Row++
	b print2For
printFor2:
	addiu $t4,$t4,1	#Column++
	b printFor
printEnd:
	jr $ra
###########################################################

###########################################################
#		Subprogram Description
#  Reads a row and column from the user, then prints
#   the element at that location or prints and error
#   if the locations is out of bounds

###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp+0  Base address (IN)
#	$sp+4  Hight (IN)
#	$sp+8  Width (IN)
###########################################################
#		Register Usage
#	$t0	Base
#	$t1	Hight
#	$t2	Width
#	$t3 Row
#	$t4 Column
#	$t5 Index
#	$t6
#	$t7
#	$t8
#	$t9
###########################################################
		.data
element_msg:	.asciiz"\nselecting..\n"
element_error:	.asciiz"\nArrayOutOfBoundsException!\n"
element_row:	.asciiz"Enter Row(START from 0): "
element_column:	.asciiz"Enter Column(START from 0): "
element_select:	.asciiz"Found Element is "
###########################################################
		.text
print_element:
	# Index address calculation:
	#  i = b + s * (e*k + n')
	lw $t0,0($sp)
	lw $t1,4($sp)
	lw $t2,8($sp)
	
	li $v0,4
	la $a0,element_msg
	syscall
	
elementEnter:
	li $v0,4
	la $a0,element_column
	syscall
	
	li $v0,5
	syscall
	move $t4,$v0
	
	bge $t4,$t2,elementError
	
	li $v0,4
	la $a0,element_row
	syscall
	
	li $v0,5
	syscall
	move $t3,$v0

	bge $t3,$t1,elementError
	
	b printElement
	
elementError:
	li $v0,4
	la $a0,element_error
	syscall
	b elementEnter
	
printElement:
	mul $t5,$t1,$t4	#Index=Height*Column
	add $t5,$t5,$t3	#Index+=Row
	sll $t5,$t5,2	#Index*=Element's Size
	add $t5,$t5,$t0	#Index+=Base
	
	li $v0,4
	la $a0,element_select
	syscall
	
	li $v0,1
	lw $a0,0($t5)
	syscall
	
	b elementEnd
elementEnd:
	jr $ra
###########################################################
