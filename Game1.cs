using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BuildADino;

public class Game1 : Game
{
    
    private GraphicsDeviceManager Graphics;
    private SpriteBatch CurrentSpriteBatch;
    private GameSession CurrentSession;
    private Renderer CurrentRenderer;
    private InputHandler CurrentInputHandler;

    private int TimeBetweenTicks = 1000; // milliseconds
    private int CurrentTimer = 0;

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        CurrentSession = new GameSession(this.Content);
        CurrentRenderer = new Renderer(GraphicsDevice, CurrentSpriteBatch, Content);
        CurrentInputHandler = new InputHandler(CurrentRenderer, CurrentSession);
    }

    protected override void LoadContent()
    {
        CurrentSpriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        CurrentTimer += gameTime.ElapsedGameTime.Milliseconds;
        if (CurrentTimer > TimeBetweenTicks) {
            CurrentTimer -= TimeBetweenTicks;
            CurrentSession.UpdateActiveTiles();
        }
        // TODO: Add your update logic here

        CurrentInputHandler.HandleAllInputs(gameTime);
        CurrentSession.TickTiles(gameTime.ElapsedGameTime.Milliseconds);


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        CurrentRenderer.Render(CurrentSession);

        base.Draw(gameTime);
    }
}
