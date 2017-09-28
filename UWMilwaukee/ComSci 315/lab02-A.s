# MIPS Assembly
# By: Yegun Kim

	.data
intro: .asciiz "Lab02 partA\n"
rectangle: .asciiz "\n Enter the rectangle's length: "
recwidth: .asciiz "\n Enter the rectangle's width: "
recArea: .asciiz "Rectangle's area = "
	.globl	main
	.text
main:

	li	$v0, 4		
	la	$a0, intro
	syscall
	
	li $v0, 4
	la $a0, rectangle
	syscall
	
	li	$v0, 5
	syscall
	
	move $t0, $v0
	
	li $v0, 4
	la $a0, recwidth
	syscall
	
	li $v0, 5
	syscall

	move $t1, $v0
	
	mult $t0,$t1
	mflo $t2
	
	li $v0, 4
	la $a0, recArea
	syscall
	
	li $v0, 1
	move $a0, $t2
	syscall
	
	li $v0,10
	syscall