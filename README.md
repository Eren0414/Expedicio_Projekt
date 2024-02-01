### Üzenet megfejtése
- Csoportba szervezzük az amatőröket és napok szerint sorba rendezzük majd a csoport kulcsát kiszedjük.
- Azután csináltunk egy listát amibe a megfejtett üzeneteket tároltuk.
- Egy foreach függvénnyel a megfejtett üzenetek karaktereit kikerestük és hozzáadtuk a megfejtéshez.
  
### Megfejtés második része
- A "#" karakternek változót adtunk, majd egy if fügvénnyel vissza adtuk a karakter stringes változóját.
- Ezután megszámoltuk mennyi "#" változó van a megfejtetlen üzenetekben és behelyettesítettük a helyes mondatok betűinek eredményével.

### Beolvasás
- Az üzenet sorait egy tömb változóval beolvastuk, majd a nap és amatőr változóknak "0"-ás értéket adtunk.
- Egy for ciklussal megadtuk a sorok hosszát és a mutató változó értékét kettő részre szedtük, majd oszlopokra osztottuk a napokat és amatőröket. A megfejtetlen üzeneteket külön sorba írtuk egy else függvénnyel és az amatorok listához hozzáadtuk.

### Napi Statisztika
- Ebben a feladatban a farkasok számát írjuk ki egy fájlban, amit megtalálhat a "napiStatisztika.txt" néven a fájl mappájában. 
- Előszőr is linq segítségével, mint az előző feladatban sorba rendezzük a napok számát és kivesszük, majd melléírjuk a rádióamatőrök számát. 
-  Regex segítségével kikeressük és csoportba rakjuk a farkasok számát, majd a napok és rádióamatőrők mellé kiírjuk őket a fájlba. Ha nem láttak farkasokat akkor azt "-" jellel jelöljük.
