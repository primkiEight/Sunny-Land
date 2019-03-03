using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToWorkWithGridAndTiles : MonoBehaviour {

    /*
     * Grid: Commponent which controls properties of the grid we will draw onto.
     * Tilemaps are children of a Grid, similar to a UI Canvas.
     * 
     * Tilemap: GameObject with components we will paint tiles onto, similar to a Layer in Photoshop.
     * 
     * Tilemap Renderer: Component which controls how tiles are rendered including sorting, material and masking.
     * 
     * Tile Palette: An asset which holds a collection of Tiles we can select from.
     * 
     * Tile: An asset which holdes a reference to a Sprite, a Color value, and a Collider type.
     * 
     * Scriptible Tiles: Tiles can be scripted in C# to create custom behaviour which executes when Tilemap is refreshed,
     * for example by drawing more tiles. Generally contain rendering and collision info.
     * 
     * Scriptible Brushes: Brushes can be scripted in C# and can execute whatever code you want when someone paints.
     * Brush code runs when painting and will not update when the Tilemap refreshes.
     * Users must repaint to make changes.
     * 
     * 2D Extras: Example Brushes and Tiles made by Unity 2D developers including
     * Rule Tiles, Animated Tiles, Radnom Tiles, Prefab Brushes and more.
     * https://github.com/Unity-Technologies/2d-extras
     * 
     */

    /* Otvoriti Window > Tile Palette.
     * U hijerarhiju dodati 2D Object > Tilemap, kreira se Grid parent i jedan Tilemap child objekt.
     * 
     * Na Gridu kroz inspektor možemo manipulirati veličinom ćelije, razmakom mežu njima i koordinatnom orijentacijom.
     * 
     * Na Tilemap objektu imamo Tilemap komponentu i Tilemap Renderer komponentu.
     * Promijenimo ime Tilemap objektu u ono što mu je namjena, npr Ground.
     * Promijenimo Layer objekta po potrebi. Unutar Tilemap Renderer definiramo Sorting Layer po potrebi.
     * 
     * Unutar Tile Palette prozora odaberemo postojeću paletu, ili kreiramo novu.
     * Kod kreiranja nove, definiramo Folder u Assetima gdje ćemo spremiti palettu.
     * 
     * Iz Tile Palette prozora kliknemo na neki od tileova, odaberemo brush alat, i na sceni po gridu crtamo potezom miša.
     * Shift dok je Brush tool i potez po gridu ili Erase tool brišu ćeliju grida.
     * Tipke Š i Đ mogu rotirati selektirani tile na brush toolu.
     * Moguće je u paleti selektirati više tileova i onda crtati u više ćelija grida jednim klikom/potezom.
     * 
     * Unutar Tile Palette prozora može se Editirati raspored tileova, brisati ih, rotirati (Š i Đ) i mijenjati ih.
     * Obavezno izaći iz Edit toola prije crtanja po gridu na sceni (inače nije moguće crtati).
     * 
     * Kada kreiramo novu palettu, u nju dodajemo tiles tako da odaberemo iz Asseta odgovarajući sprite sheet
     * ili pojedini sprite, i dovučemo ga u Tile Palette prozor. Unity će automatski kreirati Tiles folder u assets,
     * u kojem kreiramo proizvoljni Tile aset.
     * Naravno preduvjet je da je sprite sheet dobro napravljen s ispravnim dimenzijama spritea pixel po jedinici duljine.
     * 
     * Svakom tile assetu unutar Tiles foldera možemo mijenjati referencu na sprite (omogućuje globalne izmjene
     * spritea - svaki tile na gridu promijenit će svoju referencu na neki drugi definirani sprite
     * pa je lako raditi globalne izmjene na sceni - to naravno neće raditi izmjene originalnom spriteu,
     * već samo tile koji je referenciran na sprite; - možemo mijenjati color, možemo mijenjati Collider Type.
     * 
     * 
     * Na Tilemap gameobjekt dodajemo komponentu Tilemap Collider 2D. No sada nam svaki tile na gridu ima zaseban collider.
     * Dodamo i komponentu Composite Collider 2D, a njenim dodavanjem automatski se kreira komponenta Rigidbody2D,
     * koja nam je za ground potpuno nepotrebna pa njen Body Type prebacujemo u Static.
     * I dalje što moramo napraviti je na komponenti Tilemap Collider 2D postaviti da se koriste kao Composite
     * stavljanjem kvačice na Used By Composite. Sada imamo Collider koji opisuje objekte na gridu,
     * umjesto da svaki grid ima svoj zaseban collider. Također se vidi da ono što je transparentno na spriteu
     * neće biti dio collidera, dakle collider prepoznaje oblik spritea.
     * 
     * Opcija Generation Type i postavljen Synchronous nam omogućava da u play modu radimo izmjene dodavanjem
     * ili brisanjem tileova iz grida (erase tool), i automatski će se mijenjati i collider dio izmijenjene grid ćelije.
     * 
     */




}