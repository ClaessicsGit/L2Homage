echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\etcitemgrp.dat" etcitemgrp.dat

echo x|mxencdec.exe etcitemgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\etcitemgrp.ddf" -e "%~dp0l2asm-disasm\newdats\etcitemgrp-new.ddf" "etcitemgrp.dat" "%~dp0..\..\Data\decrypted client files\etcitemgrp.txt"
del etcitemgrp.dat /f /q

exit