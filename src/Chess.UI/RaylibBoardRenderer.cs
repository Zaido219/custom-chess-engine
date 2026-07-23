using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Numerics;
using Chess.Core.BoardState;
using Raylib_cs;
using Color = Raylib_cs.Color; // fixes namespace ambiguity
using Rectangle = Raylib_cs.Rectangle;
namespace Chess.UI;

public class RayLibBoardRenderer : IBoardRenderer
{
    private readonly int _squareSize = 80;
    // textures should be loaded once at startup and
    // then simply retrieve by e.g key during rendering if not
    // Raylib will attempt to load the PNG image off your 
    // hard drive and allocate new GPU memory 60 times per second per piece.
    //  Your graphics memory will explode within seconds!
    private readonly Dictionary<int, Texture2D> _pieceTextures = new();
    private static string GetAssetPath(string fileName)
    {
        // resolves path relative to the application execution directory
        return Path.Combine(AppContext.BaseDirectory, "assets", fileName);
    }
    // disposes raylib
    public void Dispose()
    {
        UnloadTextures();
        if (Raylib.IsWindowReady())
        {
            Raylib.CloseWindow();
        }
    }
    // cleaner initialization method
    public void Init()
    {
         // initialized window; if hindi pa
         // this is first step always
        if (!Raylib.IsWindowReady())
        {
            Raylib.InitWindow(640, 640, "Chess engine");
            Raylib.SetTargetFPS(60);
        }
        // then load textures
        InitializeTextures();
    }
    public void InitializeTextures()
    {
        // remember in openGl raylib load texture requires an active openGL context
        // White Pieces
    _pieceTextures[Piece.White | Piece.King]   = Raylib.LoadTexture(GetAssetPath("king-w.png"));
    _pieceTextures[Piece.White | Piece.Queen]  = Raylib.LoadTexture(GetAssetPath("queen-w.png"));
    _pieceTextures[Piece.White | Piece.Rook]   = Raylib.LoadTexture(GetAssetPath("rook-w.png"));
    _pieceTextures[Piece.White | Piece.Bishop] = Raylib.LoadTexture(GetAssetPath("bishop-w.png"));
    _pieceTextures[Piece.White | Piece.Knight] = Raylib.LoadTexture(GetAssetPath("knight-w.png"));
    _pieceTextures[Piece.White | Piece.Pawn]   = Raylib.LoadTexture(GetAssetPath("pawn-w.png"));
    // Black Pieces 
    _pieceTextures[Piece.Black | Piece.King]   = Raylib.LoadTexture(GetAssetPath("king-b.png"));
    _pieceTextures[Piece.Black | Piece.Queen]  = Raylib.LoadTexture(GetAssetPath("queen-b.png"));
    _pieceTextures[Piece.Black | Piece.Rook]   = Raylib.LoadTexture(GetAssetPath("rook-b.png"));
    _pieceTextures[Piece.Black | Piece.Bishop] = Raylib.LoadTexture(GetAssetPath("bishop-b.png"));
    _pieceTextures[Piece.Black | Piece.Knight] = Raylib.LoadTexture(GetAssetPath("knight-b.png"));
    _pieceTextures[Piece.Black | Piece.Pawn]   = Raylib.LoadTexture(GetAssetPath("pawn-b.png"));
    }
    public void Render(Board board)
    {
        // dont forget to initialize your textures
        // once raylib/opengl is active
        // stupid
        // main render loop
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            // main drawBoard function
            DrawBoard(board);
            

            Raylib.EndDrawing();
        }
        // unload textures from vram
        UnloadTextures();
        Raylib.CloseWindow();

    }
    private void DrawBoard(Board board)
    {
        for(int rank=7; rank >= 0; rank--)
        {
            for(int file=0; file < 8; file++)
            {
                int index = (rank * 8) + file;
                int piece = board[index];
                // convert board rank/file to screen pixel
                int screenX = file * _squareSize;
                int screenY = (7 - rank) * _squareSize;
                // draw tile background color
                bool isLightSquare = (rank + file) % 2 != 0;
                Color tileColor = isLightSquare ? new Color(240, 217, 181, 255) : new Color(181, 136, 99, 255);
                Raylib.DrawRectangle(screenX, screenY, _squareSize, _squareSize, tileColor);
                // render piece symbol if the piece isnt empty
                if(piece != 0)
                {
                    Texture2D texture = GetTexture(piece);
                    //Draw textures
                    // draw texture can't correctly render the pieces
                    Raylib.DrawTexturePro(
                        texture,
                        new Rectangle(0,0,texture.Width, texture.Height),
                        new Rectangle(screenX, screenY, _squareSize, _squareSize),
                        new Vector2(0,0),
                        0.0f,
                        Color.White
                    );
                }
            }
        }
    }
    // of course we also need to clean up the textures before closing raylib
    private void UnloadTextures()
    {
        foreach (var texture in _pieceTextures.Values)
        {
            // this clears vram
            // else this will stay on vram forever
            // creating a native memory leak
            Raylib.UnloadTexture(texture);
        }
        // clears ram
        // c# does this using its garbage collector
        _pieceTextures.Clear();
    }
    private Texture2D GetTexture(int piece)
    {
       // we now simply return an already laoded texture
       // gemini said this retrieval is o(1) time
       if(_pieceTextures.TryGetValue(piece, out Texture2D texture))
        {
            return texture;
        }
        throw new Exception($"No texture loaded for piece bitmask: {piece}");
    }
    //! un needed code
    // !this is a duplicated code
    private static string mapUnicode(int piece)
    {
        return piece switch
        {
            // White Pieces
        (Piece.White | Piece.King)   => "\u2654",
        (Piece.White | Piece.Queen)  => "\u2655",
        (Piece.White | Piece.Rook)   => "\u2656",
        (Piece.White | Piece.Bishop) => "\u2657",
        (Piece.White | Piece.Knight) => "\u2658",
        (Piece.White | Piece.Pawn)   => "\u2659",

        // Black Pieces
        (Piece.Black | Piece.King)   => "\u265A",
        (Piece.Black | Piece.Queen)  => "\u265B",
        (Piece.Black | Piece.Rook)   => "\u265C",
        (Piece.Black | Piece.Bishop) => "\u265D",
        (Piece.Black | Piece.Knight) => "\u265E",
        (Piece.Black | Piece.Pawn)   => "\u265F",

        // Empty square or fallback
        _ => "."
        };
    }
}