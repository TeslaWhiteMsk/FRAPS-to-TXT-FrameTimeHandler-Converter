# FRAPS-to-TXT-FrameTimeHandler-Converter

FRAPS bencmarking frametime file to TXT FrameTimeHandler file Converter Utility.

FRAPS http://www.fraps.com/

TXT FrameTimeHandler http://s8-9.narod.ru/FTHandler/FTHandler.html

Данная утилита конвертирует файлы фреймтайма бенчмарка Fraps формата:

n,             Frametime(n)

n+1,       Frametime(n+(n+1))

...

n+..., Frametime(n+...+(n+...)) 

В формат утилиты TXT FrameTimeHandler:

"FrameTime(n)"

"FrameTime(n+1)"

...

"FrameTime(n+...)"

Где: FrameTime(n) - Время кадра n.


