_DATA SEGMENT
_DATA ENDS
_TEXT SEGMENT
_TEXT ENDS

.code 
PUBLIC calculateDistanceASM

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

	mov rax, 0
	add rax, rcx
	add rax, rdx

    ret

calculateDistanceASM ENDP


END