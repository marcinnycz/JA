PUBLIC calculateHDistanceAsm

.code 

calculateHDistanceAsm PROC

mov rbx, 0

    mov r9, 0
r9_loop:
    mov r10, 0
r10_loop:
    

    mov r11, r9
    mov r12, r10

    mov rax, rcx
	cmp  r9, rcx       
    jb   endX_is_bigger
    xchg rcx, r9
endX_is_bigger:
    sub rcx, r9
    mov r9, rax

    mov rax, rdx
	cmp  r10, rdx
    jb   endY_is_bigger
    xchg rdx, r10
endY_is_bigger:
    sub rdx, r10
    mov r10, rax

    cmp  rcx, rdx
    jb   rdx_is_bigger2
    xchg rcx, rdx
rdx_is_bigger2:
    sub rdx, rcx


    imul rdx, 10
    imul rcx, 14
    

	mov rax, 0
	add rax, rcx
	add rax, rdx

    mov [r8 + rbx], rax
    add rbx, 4

    mov rcx, r9
    mov rdx, r10
    mov r9, r11
    mov r10, r12
    

    
    add r10, 1
    cmp r10, 50
    jne r10_loop

    add r9, 1
    cmp r9, 50
    jne r9_loop

    ret

calculateHDistanceAsm ENDP

END