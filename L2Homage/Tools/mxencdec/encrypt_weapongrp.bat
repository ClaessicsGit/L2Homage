"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\weapongrp-new.ddf" "%~dp0..\..\Data\decrypted client files\weapongrp.txt" "weapongrp.dat"
echo x|mxencdec.exe weapongrp.dat
move weapongrp.dat "%~dp0..\..\Export\Client"