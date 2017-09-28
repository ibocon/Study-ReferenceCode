###########################################################
#		Program Description
#Program 03
#Call create_array
#OUT Base(sp+0), Length(sp+4)
#Store Base, Length into static memory
#Call print_array
#IN Base, Length
#Call stats

###########################################################
#		Register Usage
#	$t0 BaseAddress
#	$t1 ArrayLength
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
array: .word 0
length: .word 0
###########################################################
		.text
main:
#Start create_array
	addiu $sp,$sp,-4	#R main
	sw $ra,0($sp)
	addiu $sp,$sp,-8	#OUT Base, Length
	
	jal create_array
	
	la $t0,array	#load BaseAddress to array
	lw $t0,0($sp)
	
	la $t1,length	#load ArrayLength to length
	lw $t1,4($sp)
	
	addiu $sp,$sp,8
	
	lw $ra,0($sp)
	addiu $sp,$sp,4
	
#Start print_array
	addiu $sp,$sp,-4	#R main
	sw $ra,0($sp)
	
	addiu $sp,$sp,-8
	sw $t0,0($sp)
	sw $t1,4($sp)
	
	addiu $sp,$sp,-8	#IN Base, Length
	sw $t0,0($sp)
	sw $t1,4($sp)
	
	jal print_array
	
	lw $t0,0($sp)
	lw $t1,4($sp)
	addiu $sp,$sp,8

	addiu $sp,$sp,8
	
	lw $ra,0($sp)
	addiu $sp,$sp,4
	
#Start stats
	addiu $sp,$sp,-4
	sw $ra,0($sp)
	
	addiu $sp,$sp,-8
	sw $t0,0($sp)	#sp+0=BaseAddress
	sw $t1,4($sp)	#sp+4=ArrayLength
	
	jal stats
	
	addiu $sp,$sp,8
	
	lw $ra,0($sp)
	addiu $sp,$sp,4
	
	li $v0, 10		#End Program
	syscall
###########################################################
###########################################################
#		Subprogram Description
#create_array
#IN null
#OUT base, length
###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp		baseAddress(OUT)
#	$sp+4	arrayLength(OUT)
#	$sp+8
#	$sp+12
###########################################################
#		Register Usage
#	$t0 baseAddress
#	$t1 arrayLength
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
arrayLen_p:	.asciiz"\nEnter an array length: "
createError_p:	.asciiz"\nInvalid array length!"
###########################################################
		.text
# Ask the user for an array length
create_array:
	li $v0,4
	la $a0,arrayLen_p
	syscall

	li $v0,5
	syscall
	move $t1,$v0	#t1=arrayLength
	
	blez $t1,createError
	sw $t1,4($sp)	#sp+4=arrayLength
	
	b creating
	
createError:
	li $v0,4
	la $a0,createError_p
	syscall
	b create_array
	
creating:
	# Allocate space for the array
	sll $a0,$t1,2
	li $v0,9
	syscall
	
	move $t0,$v0
	sw $t0,0($sp)	#sp+0 = baseAddress
	
#Start read_array
	addiu $sp,$sp,-4
	sw $ra,0($sp)
	
	#I already save BaseAdress(sp+0) and ArrayLength(sp+4)
	#Therefore, I do not need to back up!
	
	addiu $sp,$sp,-8
	sw $t0,0($sp)
	sw $t1,4($sp)
	
	jal read_array
	
	addiu $sp,$sp,8
	lw $ra,0($sp)
	addiu $sp,$sp,4
	
	jr $ra	#return to calling location
###########################################################


###########################################################
#		Subprogram Description
#read_array
#single precision numbers
###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp		BaseAddress
#	$sp+4	ArrayLength
#	$sp+8
#	$sp+12
###########################################################
#		Register Usage
#	$t0	BaseAddress
#	$t1 ArrayLength
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
enterVal_p:	.asciiz "\nEnter a single precision number: "
###########################################################
		.text
read_array:
	lw $t0,0($sp)	#t0=BaseAddress
	lw $t1,4($sp)	#t1=ArrayLength
readFor:
	blez $t1,readEnd
	
	li $v0,4
	la $a0,enterVal_p
	syscall

	li $v0,6
	syscall

	s.s $f0,0($t0)	

	addi $t0,$t0,4
	addi $t1,$t1,-1
	
	b readFor
readEnd:
	jr $ra	#return to calling location
###########################################################

###########################################################
#		Subprogram Description
#print array
###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp		BaseAddress
#	$sp+4	ArrayLength
#	$sp+8
#	$sp+12
###########################################################
#		Register Usage
#	$t0
#	$t1
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
printVal_p:	.asciiz "\nPrint an integer value: "
###########################################################
		.text
print_array:
	# Print all values from an array
	lw $t0,0($sp)
	lw $t1,4($sp)
printFor:
	blez $t1,printEnd
	
	li $v0,4
	la $a0, printVal_p
	syscall
	
	li $v0,2
	l.s $f12,0($t0)
	syscall
	
	addi $t0,$t0,4
	addi $t1,$t1,-1

	b printFor
printEnd:
	jr $ra	#return to calling location
###########################################################

###########################################################
#		Subprogram Description
#stats
#total,count,average,maximum,minimum
###########################################################
#		Arguments In and Out of subprogram
#
#	$a0
#	$a1
#	$a2
#	$a3
#	$v0
#	$v1
#	$sp		BaseAddress
#	$sp+4	ArrayLength
#	$sp+8
#	$sp+12
###########################################################
#		Register Usage
#	$t0 BaseAddress
#	$t1 ArrayLength
#	$t2 
#	$t3
#	$t4
#	$t5 
#	$t6 
#	$t7 
#	$t8 
#	$t9	
#	$f0|$f1   Element
#	$f2|$f3   Total
#	$f4|$f5   Count
#	$f6|$f7   Average
#	$f8|$f9   Maximum
#	$f10|$f11 Minimum
###########################################################
		.data
total_p: .asciiz"\nTotal: "
count_p: .asciiz"\nCount: "
avg_p: .asciiz"\nAverage: "
max_p: .asciiz"\nMaximum: "
min_p: .asciiz"\nMinimum: "
###########################################################
		.text
stats:
	lw $t0,0($sp)
	lw $t1,4($sp)

	#initialize values
	l.s $f0,0($t0)
	mov.s $f8,$f0
	mov.s $f10,$f0
	
	li $t8,1
	mtc1 $t8,$f12
	cvt.s.w $f14,$f12
	
statsFor:
	blez $t1,statsEnd
	l.s $f0,0($t0)
	
	add.s $f2,$f2,$f0	#total+=element
	
	add.s $f4,$f4,$f14
	addi $t0,$t0,4
	addi $t1,$t1,-1
	
	c.le.s $f8,$f0
	bc1f changeMax
	c.lt.s $f10,$f0
	bc1t changeMin
	
	b statsFor
changeMax:
	mov.s $f8,$f0
	b statsFor
changeMin:
	mov.s $f10,$f0
	b statsFor
statsEnd:
	div.s $f6,$f2,$f4
	
	li $v0,4
	la $a0,total_p
	syscall
	
	li $v0,2
	mov.s $f12,$f2
	syscall
	
	li $v0,4
	la $a0,count_p
	syscall
	
	li $v0,2
	mov.s $f12,$f4
	syscall
	
	li $v0,4
	la $a0,avg_p
	syscall
	
	li $v0,2
	mov.s $f12,$f6
	syscall
	
	li $v0,4
	la $a0,max_p
	syscall
	
	li $v0,2
	mov.s $f12,$f10
	syscall
	
	li $v0,4
	la $a0,min_p
	syscall
	
	li $v0,2
	mov.s $f12,$f8
	syscall
	
	jr $ra	#return to calling location
###########################################################

