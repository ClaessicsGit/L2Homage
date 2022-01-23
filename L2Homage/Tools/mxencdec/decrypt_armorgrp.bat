echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\armorgrp.dat" armorgrp.dat

echo x|mxencdec.exe armorgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\armorgrp.ddf" -e "%~dp0l2asm-disasm\newdats\armorgrp-new.ddf" "armorgrp.dat" "%~dp0..\..\Data\decrypted client files\Armorgrp.txt"
del armorgrp.dat /f /q

exit