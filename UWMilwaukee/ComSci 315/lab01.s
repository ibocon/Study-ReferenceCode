# Hello World in MIPS Assembly
# By: Yegun Kim


        .data        # Data declaration section
hello_msg:   .asciiz "Hello World!\n";

        .text

main:                # Start of code section

        # Load the address of the message
        # into the $a0 register. Then load 4 into

        # the $v0 register to tell the processor
        # that you want to print a string.
        la $a0, hello_msg
        li $v0, 4
        syscall
        
        # Now do a graceful exit
        li $v0, 10
        syscall

        
        

# END OF PROGRAM?