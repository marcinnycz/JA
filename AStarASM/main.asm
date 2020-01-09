PUBLIC calculateDistanceASM

.code 

calculateDistanceASM PROC

	cmp  rcx, r8
    jb   r8_is_bigger
    xchg rcx, r8
r8_is_bigger:
    xchg rcx, r8

    sub rcx, r8

	cmp  rdx, r9
    jb   r9_is_bigger
    xchg rdx, r9
r9_is_bigger:
    xchg rDx, r9

    sub rdx, r9

    cmp  rcx, rdx
    jb   rdx_is_bigger2
    xchg rcx, rdx
rdx_is_bigger2:
    xchg rcx, rdx

    sub rcx, rdx


	mov rax, 0
	add rax, rcx
	add rax, rdx

    ret

calculateDistanceASM ENDP


END