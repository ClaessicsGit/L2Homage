echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\mobskillanimgrp.dat" mobskillanimgrp.dat

echo x|mxencdec.exe mobskillanimgrp.dat

"%~dp0l2asm-disasm\l2disasm.exe" -d "%~dp0l2asm-disasm\dats\mobskillanimgrp.ddf" -e "%~dp0l2asm-disasm\newdats\mobskillanimgrp-new.ddf" "mobskillanimgrp.dat" "%~dp0..\..\Data\decrypted client files\mobskillanimgrp.txt"
del mobskillanimgrp.dat /f /q

exit