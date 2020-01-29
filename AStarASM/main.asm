PUBLIC calculateHDistanceAsm

.data

rbxSave dq 0                ;Deklaracja zmiennej przechowuj¹cej wartoœæ rejestru rbx
r12Save dq 0                ;Deklaracja zmiennej przechowuj¹cej wartoœæ rejestru r12

.code 

                            ;Funkcja obliczaj¹ca odleg³oœæ wszystkich punktów na planszy 50x50 od punktu koñcowego
                            ;Przyjmuje 3 argumenty
                            ;Arg1: wspó³rzêdna X punktu koñcowego - rejestr rcx
                            ;Arg2: wspó³rzêdna Y punktu koñcowego - rejestr rdx
                            ;Arg3: wskaŸnik na tablicê 4 bajtowych wartoœci (int) - rejestr r8
                            ;Wynik zapisywany jest w tablicy z Arg3
calculateHDistanceAsm PROC

mov rbxSave, rbx            ;Zapisanie wartoœci rejstru rbx
mov r12Save, r12            ;Zapisanie wartoœci rejstru r12

mov rbx, 0                  ;Zerowanie rejestru przechowuj¹cego offset wskaŸnika tablicy wynikowej

                            ;Pêtla zagnie¿d¿ona, przechodz¹ca po wspó³rzêdnych wszystkich punktów na planszy
    mov r9, 0               ;Zerowanie rejestru licznika pêtli zewnêtrznej
r9_loop:                    ;Pocz¹tek pêtli zewnêtrznej
    mov r10, 0              ;Zerowanie rejestru licznika pêtli wewnêtrznej
r10_loop:                   ;Pocz¹tek pêtli wewnêtrznej
    
    mov r11, r9             ;Zapisanie stanu liczników w rejestrach pomocniczych
    mov r12, r10

    mov rax, rcx            ;Zapisanie wartoœci wspó³rzêdnej X punktu koñcowego w rejestrze pommocniczym
	cmp  r9, rcx            ;Sprawdzenie czy wspó³rzêdna X aktualnego punktu jest wiêksza od wspó³rzêdnej X punktu koñcowego
    jb   endX_is_bigger
    xchg rcx, r9
endX_is_bigger:
    sub rcx, r9             ;Wyliczenie odleg³oœci miêdzy punktami na osi X
    mov r9, rax             ;Przemieszczenie wartoœci wpó³rzêdniej X do rejestru pomocniczego

    mov rax, rdx            ;Zapisanie wartoœci wspó³rzêdnej Y punktu koñcowego w rejestrze pommocniczym
	cmp  r10, rdx           ;Sprawdzenie czy wspó³rzêdna Y aktualnego punktu jest wiêksza od wspó³rzêdnej Y punktu koñcowego
    jb   endY_is_bigger
    xchg rdx, r10
endY_is_bigger:
    sub rdx, r10            ;Wyliczenie odleg³oœci miêdzy punktami na osi Y
    mov r10, rax            ;Przemieszczenie wartoœci wpó³rzêdniej y do rejestru pomocniczego

    cmp  rcx, rdx           ;Sprawdzenie czy odleg³oœæ na osi X jest wiêksza od odleg³oœci na osi Y
    jb   rdx_is_bigger2
    xchg rcx, rdx
rdx_is_bigger2:
    sub rdx, rcx            ;Wyliczenie ró¿nicy miêdzy odleg³oœciami


    imul rdx, 10            ;Ró¿nica mno¿ona jest razy 10 (krok po osi X lub Y)
    imul rcx, 14            ;Mniejsza odleg³oœæ mno¿ona jest razy 14 (krok pod k¹tem)
    

	mov rax, 0              ;Sumowanie przemno¿onych wartoœci
	add rax, rcx
	add rax, rdx

    mov [r8 + rbx], rax     ;Zapisanie wyniku do tablicy
    add rbx, 4              ;Przesuniêcie wskaŸnika tablicy

    mov rcx, r9             ;Przywrócenie wartoœci wspó³rzêdnej X punktu koñcowego
    mov rdx, r10            ;Przywrócenie wartoœci wspó³rzêdnej Y punktu koñcowego
    mov r9, r11             ;Przywrócenie wartoœci licznika pêtli zewnêtrznej
    mov r10, r12            ;Przywrócenie wartoœci licznika pêtli wewnêtrznej
    
    add r10, 1              ;Inkrementacja licznika pêtli wewnêtrznej
    cmp r10, 50             ;Koniec pêtli wewnêtrznej
    jne r10_loop

    add r9, 1               ;Inkrementacja licznika pêtli zewnêtrznej
    cmp r9, 50              ;Koniec pêtli zewnêtrznej
    jne r9_loop
    
    mov rbx, rbxSave        ;Przywrócenie wartoœci rejstru rbx
    mov r12, r12Save        ;Przywrócenie wartoœci rejstru r12

    ret                     ;Koniec procedury

calculateHDistanceAsm ENDP


                            ;Funkcja testuj¹ca dzia³anie instrukcji typu SIMD
                            ;Funkcja dodaje do siebie dwie tablice zmiennych zmiennoprzecinkowych pojedynczej precyzji (float)
                            ;Przyjmuje 4 argumenty
                            ;Arg1: wskaŸnik na pierwsz¹ tablicê danych - rejestr rcx
                            ;Arg2: wskaŸnik na drug¹ tablicê danych - rejestr rdx
                            ;Arg3: wskaŸnik na tablicê wynikow¹ - rejestr r8
                            ;Arg4: rozmiar tablic (w bajtach) - rejestr r9
                            ;Wynik zapisywany jest w tablicy z Arg3
SIMDExample PROC

    mov rax, 0              ;Zerowanie licznika pêtli

raxLoop:                    ;Pocz¹tek pêtli
    movups xmm0, [rcx + rax];Wczytanie 4 wartoœci typu float do rejestru xmm0
    movups xmm1, [rdx + rax];Wczytanie 4 wartoœci typu float do rejestru xmm1
    addps xmm0, xmm1        ;Jednoczesne sumowanie wszystkich 4 wartoœci
    movdqu [r8 + rax], xmm0 ;Zapisanie wyniku do tablicy
    add rax, 16             ;Inkrementacja licznika
    cmp rax, r9             ;Koniec pêtli
    jne raxLoop

    ret                     ;Koniec procedury

SIMDExample ENDP

END