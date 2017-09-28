# MIPS Assembly
# By: Yegun Kim

	.data
intro: .asciiz "Lab02 partB\n"
base: .asciiz "\n Enter the Right triangle's base: "
height: .asciiz "\n Enter the Right triangle's height: "
triArea: .asciiz "Right triangle's area = "
remain0: .asciiz ".0"
remain1: .asciiz ".5"
	.globl	main
	.text
main:

	li	$v0, 4		#print intro
	la	$a0, intro
	syscall
	
	li $v0, 4		#print base
	la $a0, base
	syscall
	
	li	$v0, 5		#read base
	syscall
	
	move $t0, $v0 	#t0=base
	
	li $v0, 4		#print height
	la $a0, height
	syscall
	
	li $v0, 5		#read height
	syscall

	move $t1, $v0	#t1=height
	
	mult $t0, $t1
	mflo $t2		#t2= base*height
	
	li $s0, 2
	div $t2, $s0	#(base*height)/2
	mflo $t3		#t3= quotient
	mfhi $t4		#t4= remainder
	
	li $v0, 4		#print triArea
	la $a0, triArea
	syscall
	
	li $v0, 1
	move $a0, $t3
	syscall
	
	bgtz $t4, else	#if statement
	li $v0, 4		#remainder<=0
	la $a0, remain0
	syscall    
	b next
else:				#remainder>0
	li $v0, 4		
	la $a0, remain1
	syscall
next:
	
	li $v0,10		#End
	syscall
	
	
	
		
