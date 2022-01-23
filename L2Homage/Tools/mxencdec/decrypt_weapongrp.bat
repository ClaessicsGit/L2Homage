echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\weapongrp.dat" weapongrp.dat

echo x|mxencdec.exe weapongrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\weapongrp.ddf" -e "%~dp0l2asm-disasm\newdats\weapongrp-new.ddf" "weapongrp.dat" "%~dp0..\..\Data\decrypted client files\weapongrp.txt"
del weapongrp.dat /f /q

exit