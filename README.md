# Blockwoche_Spieleentwicklung
Spiel im Bereich der Vorlesung Spieleentwicklung SS2018
Kurze Erklärung des Spiels:
Idee war, eine Art Stickfight-Spiel mit der "Höher"-Vorgabe zu erstellen.
Ziel des Spiels ist es, nach Ablauf der Zeit der höchste Spieler zu sein, oder die Gegner aus dem Spielbereich zu drängen.
Dies kann dadurch passieren, dass man sich selber so hoch baut, dass die Gegenspieler nicht mehr im angezeigten Spielbereich sind.
Um sich nach oben zu bauen hat man verschieden Große Objekte, die man sich so anordnen kann wie man will.
Dabei besitzen größere Objekte eine höhere Bau-Abklingzeit als kleinere Objekte.
Die gebauten Objekte können durch Schüsse zerstört werden, oder mit Bomben-Powerups aus dem Spielbereich gestoßen werden.
Spiel starten:
Im Anfangsscreen kann die Spieleranzahl ausgewählt werden. Spieler 1 ist immer mit der Eingabe von Maus und Tastatur,
alle weiteren Mitspieler werden per Controller-Input gesteuert.
Mit einem Klick auf Start startet das Spiel sofort, ein "Pseudo-Random"-Level wird generiert und die Zeit beginnt abzulaufen.
Steuerung:
							Tastatur:			Controller:
Fadenkreuz-Steuerung:		Maus				Rechter Analogstick
Laufen(rechts,links)		A,D					Linker Analogstick
Springen:					Leertaste			Button A
Bauteile durchwechseln:		Q,E					LB, RB
Ausgewähltes Teil bauen:	Rechte Maustaste	LT
Bauen abbrechen:			C					Button B
Schießen:					Linke Maustaste		RT

Immer wieder fallen Kisten vom Himmel. Sammelt man diese ein, bekommt man eine Bombe anstatt eines normalen Schusses.
Diese Bombe ersetzt einen normalen Schuss und kann belibig gestackt werden.
Bomben verursachen Flächenschaden und schleudern Bauteile, Spieler und Lootboxen fort.

Verwendete Assets und Codefragmente:
Als Charakter wurde der Standart-2D Charakter aus den Standard Assets verwendet.
Den Controller haben wir auf unsere Bedürfnisse angepasst.
Für das Level-Design und Lootboxen wurden Assets aus dem BayatGames-Pack verwendet.
Die Schüsse und Bomben sind einfache Sprites, die aus dem ParticleBulletSystem genommen wurden.