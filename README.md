# 2026_1-2-16

# AR Shield Defense – Meta Quest 3  
Autors: Àlex Mitjans i Àngela Giribet  
Assignatura: Sistemes Gràfics Interactius (SGI)

## 1. Objectiu del projecte
Aquest projecte és un joc de Realitat Augmentada per a Meta Quest 3 en què l’usuari ha de defensar-se d’un enemic virtual que apareix aleatòriament a diferents punts de la sala real.

L’enemic dispara projectils de diversos colors i l’usuari ha de bloquejar-los amb un escut virtual, seleccionant el color correcte mitjançant un menú radial controlat amb gestos de la mà (pinch).

El projecte integra passthrough, detecció de l’entorn amb la malla del Meta Quest 3, reconeixement de mans i un sistema de control interactiu, complint tots els requisits de la pràctica.

---

## 2. Elements principals del projecte

### 2.1 XR Rig i entorn de Realitat Mixta
**XR Origin / XR Rig**  
Representa la posició i orientació de l’usuari. Inclou els components necessaris per activar el passthrough i el seguiment de mans.

**Hands (Left i Right)**  
Reben la informació del hand tracking.  
- L’escut s’ancora a la mà dreta.  
- El menú radial utilitza la direcció de la mà esquerra per determinar la selecció.  
- El gest de pinch confirma el color seleccionat.

**Effect Mesh**  
Malla generada pel Meta Quest 3 que representa l’entorn real.  
S’utilitza per col·locar l’enemic en posicions coherents, evitant superfícies no accessibles.

---

### 2.2 Escut
**Shield (Prefab)**  
Objecte virtual utilitzat per bloquejar projectils. Es mou de manera coherent amb la mà dreta.

**Scripts associats**  
- **ShieldColor**: Gestiona el color actual de l’escut mitjançant un índex. Els projectils utilitzen aquest índex per determinar si han de ser bloquejats.

---

### 2.3 Gestió de l’escut
**ShieldManager**  
Instancia i configura l’escut, ancorant-lo a la mà dreta amb els offsets necessaris.

**Scripts associats**  
- **ShieldController**:  
  - Instancia l’escut com a fill de la mà dreta.  
  - Actualitza la posició i rotació locals.  
  - Rep l’índex del color seleccionat i el transmet al component ShieldColor.

---

### 2.4 Menú radial de selecció de color
**Radial Menu (GameObject)**  
Interfície circular amb totes les opcions de color disponibles.

**Radial Part (Prefab)**  
Cada secció representa un color.

**Scripts associats**  
- **RadialSelection**:  
  - Calcula la direcció de la mà respecte al menú.  
  - Determina la secció seleccionada.  
  - Amb un pinch, confirma la selecció i envia l’índex al ShieldController.

---

### 2.5 Gestió dels gestos
**GestureManager**  
Gestiona la interacció amb el menú radial mitjançant la mà esquerra. Manté el menú davant del palmell i detecta el pinch.

**Scripts associats**  
- **GestureRadialMenuController**:  
  - Actualitza la posició i orientació del menú.  
  - Detecta el pinch del dit índex.  
  - Envia l’índex seleccionat al sistema de colors.

---

### 2.6 Fantasma (Enemic)
**Ghost (Prefab)**  
Enemic principal del joc. Es teletransporta periòdicament a posicions aleatòries de l’entorn real utilitzant la malla del Effect Mesh. Dispara projectils cap a l’usuari.

**Scripts associats**  
- **GhostSpawner**:  
  - Instancia el fantasma.  
  - El teletransporta a ubicacions vàlides.  
  - L’orienta cap a l’usuari.  
  - Gestiona el ritme d’atac i dispara projectils dirigits al cap del jugador.

---

### 2.7 Projectils
**SlowProjectile (Prefab)**  
Projectils lents disparats per l’enemic. Cada projectil té un color associat.

**Scripts associats**  
- **SlowProjectile**:  
  - Controla el moviment i les col·lisions.  
  - Si el color coincideix amb el de l’escut, el projectil es destrueix i genera un efecte d’explosió.

**Nota:**  
La intenció inicial era generar projectils de colors aleatoris mitjançant el ColorManager. Per manca de temps, aquesta funcionalitat no es va completar i actualment tots els projectils són de color roig.

**Efectes visuals**  
- **ExplosionVFX (Prefab)**: Efecte visual que apareix quan un projectil és bloquejat correctament.

---

### 2.8 Gestió de colors
**ColorManager**  
Centralitza la definició dels colors disponibles.  
Proporciona un mètode per obtenir un color a partir d’un índex, garantint coherència entre l’escut, el menú radial i els projectils.

---

## 3. Compliment dels requisits de la pràctica
- Passthrough: l’usuari veu l’entorn real en tot moment.  
- Reconeixement de l’escena: ús de la malla del Meta Quest 3 per col·locar l’enemic.  
- Reconeixement de mans: control de l’escut i selecció de color amb gestos.  
- Mecanisme de control: menú radial interactiu que afecta directament la jugabilitat.


