###########################################################
#		Program Description

###########################################################
#		Register Usage
#	$t0	variable
#	$t1	-927
#	$t2 -45
#	$t3 150
#	$t4 sum
#	$t5	count
#	$t6 remainder
#	$t7 quotient
#	$t8
#	$t9
###########################################################
		.data
intro:	.asciiz "\nProgram 1:\n\n"
msg:	.asciiz"\nEnter an integer temperature between -45 and 150 degrees Farenheit or -927: "
e:		.asciiz"\nInvalid temperature. Try Again!!"
avg:	.asciiz"\n\nThe integer average temperatures is: "
remain:	.asciiz"\nThe remainder is: ";
nothing:.asciiz"\nNo valid temperatures were entered."
test:.asciiz"\n"
###########################################################
		.text
main:
	li $t1, -927				#t1=-927
	li $t2, -45					#t2=-45
	li $t3, 150					#t3=150
	li $t4, 0					#t4=0
	li $t5, 0					#t5=0
	
	li $v0,4					#print intro msg
	la $a0,intro
	syscall
	
	li $v0,4					#print message
	la $a0,msg
	syscall
	
	li $v0,	5					#read integer into $t0
	syscall
	move $t0, $v0
	
	beq $t0,$t1,n				#if($t0==-927) branch nothing
					
	b check						#check variable

check:
	blt $t0,$t2,error			#if(variable<-45) branch error
	bgt $t0,$t3,error			#if(variable>150) branch error

	add $t4,$t4,$t0				#sum+=variable
	addi $t5, 1					#count++

	b while

error:
	li $v0, 4					#print(error)
	la $a0, e
	syscall
	
	b while
	
while:
	li $v0,4					#print message
	la $a0,msg
	syscall
	
	li $v0,	5					#read integer into $t0
	syscall
	move $t0, $v0
	
	beq $t0,$t1,a				#if($t0==-927) branch avg
	b check
	
a:
	div $t4,$t5					#sum/count=average=quotient
	mfhi $t6					#t6=remainder
	mflo $t7					#t7=quotient

	li $v0,4					#print average= quotient of sum/count
	la $a0, avg
	syscall
	
	li $v0, 1
	move $a0,$t7
	syscall
	
	li $v0,4					#print average= reminder of sum/count
	la $a0, remain
	syscall
	
	li $v0, 1
	move $a0,$t6
	syscall
	
	li $v0, 10					#End Program
	syscall
	
n:
	li $v0, 4					#print(nothing)
	la $a0, nothing
	syscall
	
	li $v0, 10					#End Program
	syscall
###########################################################

