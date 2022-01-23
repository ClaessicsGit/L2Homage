echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\skillgrp.dat" skillgrp.dat

echo x|mxencdec.exe skillgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\skillgrp.ddf" -e "%~dp0l2asm-disasm\newdats\skillgrp-new.ddf" "skillgrp.dat" "%~dp0..\..\Data\decrypted client files\skillgrp.txt"
del skillgrp.dat /f /q

exit