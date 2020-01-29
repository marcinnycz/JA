PUBLIC calculateHDistanceAsm

.data

rbxSave dq 0                ;Deklaracja zmiennej przechowuj�cej warto�� rejestru rbx
r12Save dq 0                ;Deklaracja zmiennej przechowuj�cej warto�� rejestru r12

.code 

                            ;Funkcja obliczaj�ca odleg�o�� wszystkich punkt�w na planszy 50x50 od punktu ko�cowego
                            ;Przyjmuje 3 argumenty
                            ;Arg1: wsp�rz�dna X punktu ko�cowego - rejestr rcx
                            ;Arg2: wsp�rz�dna Y punktu ko�cowego - rejestr rdx
                            ;Arg3: wska�nik na tablic� 4 bajtowych warto�ci (int) - rejestr r8
                            ;Wynik zapisywany jest w tablicy z Arg3
calculateHDistanceAsm PROC

mov rbxSave, rbx            ;Zapisanie warto�ci rejstru rbx
mov r12Save, r12            ;Zapisanie warto�ci rejstru r12

mov rbx, 0                  ;Zerowanie rejestru przechowuj�cego offset wska�nika tablicy wynikowej

                            ;P�tla zagnie�d�ona, przechodz�ca po wsp�rz�dnych wszystkich punkt�w na planszy
    mov r9, 0               ;Zerowanie rejestru licznika p�tli zewn�trznej
r9_loop:                    ;Pocz�tek p�tli zewn�trznej
    mov r10, 0              ;Zerowanie rejestru licznika p�tli wewn�trznej
r10_loop:                   ;Pocz�tek p�tli wewn�trznej
    
    mov r11, r9             ;Zapisanie stanu licznik�w w rejestrach pomocniczych
    mov r12, r10

    mov rax, rcx            ;Zapisanie warto�ci wsp�rz�dnej X punktu ko�cowego w rejestrze pommocniczym
	cmp  r9, rcx            ;Sprawdzenie czy wsp�rz�dna X aktualnego punktu jest wi�ksza od wsp�rz�dnej X punktu ko�cowego
    jb   endX_is_bigger
    xchg rcx, r9
endX_is_bigger:
    sub rcx, r9             ;Wyliczenie odleg�o�ci mi�dzy punktami na osi X
    mov r9, rax             ;Przemieszczenie warto�ci wp�rz�dniej X do rejestru pomocniczego

    mov rax, rdx            ;Zapisanie warto�ci wsp�rz�dnej Y punktu ko�cowego w rejestrze pommocniczym
	cmp  r10, rdx           ;Sprawdzenie czy wsp�rz�dna Y aktualnego punktu jest wi�ksza od wsp�rz�dnej Y punktu ko�cowego
    jb   endY_is_bigger
    xchg rdx, r10
endY_is_bigger:
    sub rdx, r10            ;Wyliczenie odleg�o�ci mi�dzy punktami na osi Y
    mov r10, rax            ;Przemieszczenie warto�ci wp�rz�dniej y do rejestru pomocniczego

    cmp  rcx, rdx           ;Sprawdzenie czy odleg�o�� na osi X jest wi�ksza od odleg�o�ci na osi Y
    jb   rdx_is_bigger2
    xchg rcx, rdx
rdx_is_bigger2:
    sub rdx, rcx            ;Wyliczenie r�nicy mi�dzy odleg�o�ciami


    imul rdx, 10            ;R�nica mno�ona jest razy 10 (krok po osi X lub Y)
    imul rcx, 14            ;Mniejsza odleg�o�� mno�ona jest razy 14 (krok pod k�tem)
    

	mov rax, 0              ;Sumowanie przemno�onych warto�ci
	add rax, rcx
	add rax, rdx

    mov [r8 + rbx], rax     ;Zapisanie wyniku do tablicy
    add rbx, 4              ;Przesuni�cie wska�nika tablicy

    mov rcx, r9             ;Przywr�cenie warto�ci wsp�rz�dnej X punktu ko�cowego
    mov rdx, r10            ;Przywr�cenie warto�ci wsp�rz�dnej Y punktu ko�cowego
    mov r9, r11             ;Przywr�cenie warto�ci licznika p�tli zewn�trznej
    mov r10, r12            ;Przywr�cenie warto�ci licznika p�tli wewn�trznej
    
    add r10, 1              ;Inkrementacja licznika p�tli wewn�trznej
    cmp r10, 50             ;Koniec p�tli wewn�trznej
    jne r10_loop

    add r9, 1               ;Inkrementacja licznika p�tli zewn�trznej
    cmp r9, 50              ;Koniec p�tli zewn�trznej
    jne r9_loop
    
    mov rbx, rbxSave        ;Przywr�cenie warto�ci rejstru rbx
    mov r12, r12Save        ;Przywr�cenie warto�ci rejstru r12

    ret                     ;Koniec procedury

calculateHDistanceAsm ENDP


                            ;Funkcja testuj�ca dzia�anie instrukcji typu SIMD
                            ;Funkcja dodaje do siebie dwie tablice zmiennych zmiennoprzecinkowych pojedynczej precyzji (float)
                            ;Przyjmuje 4 argumenty
                            ;Arg1: wska�nik na pierwsz� tablic� danych - rejestr rcx
                            ;Arg2: wska�nik na drug� tablic� danych - rejestr rdx
                            ;Arg3: wska�nik na tablic� wynikow� - rejestr r8
                            ;Arg4: rozmiar tablic (w bajtach) - rejestr r9
                            ;Wynik zapisywany jest w tablicy z Arg3
SIMDExample PROC

    mov rax, 0              ;Zerowanie licznika p�tli

raxLoop:                    ;Pocz�tek p�tli
    movups xmm0, [rcx + rax];Wczytanie 4 warto�ci typu float do rejestru xmm0
    movups xmm1, [rdx + rax];Wczytanie 4 warto�ci typu float do rejestru xmm1
    addps xmm0, xmm1        ;Jednoczesne sumowanie wszystkich 4 warto�ci
    movdqu [r8 + rax], xmm0 ;Zapisanie wyniku do tablicy
    add rax, 16             ;Inkrementacja licznika
    cmp rax, r9             ;Koniec p�tli
    jne raxLoop

    ret                     ;Koniec procedury

SIMDExample ENDP

END