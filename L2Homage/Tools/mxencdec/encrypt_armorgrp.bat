"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\armorgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\Armorgrp.txt" "armorgrp.dat"
echo x|mxencdec.exe armorgrp.dat
move armorgrp.dat "%~dp0..\..\Export\Client"