using System.Net.Mime;
using System;
using System.Numerics;
using Raylib_cs;

namespace mitt_spel
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1920, 1000, "Mitt Spel");

            //-------------------------------------------POSSIBILITIES-------------------------------------------

            //KLAR//få att yesButton endast gäller när mus positionen är på knappen 

            //fundera på levlar och fiender osv

            //göra pixelart till marken

            //göra så att det finns andra former av playermovement osv när man tex plockar upp ett par vingar och flyger iväg

            //få kameran att fungera 

            //border control på alla sidor 

            //samla pengar; när man har tillräckligt med pengar kan man köpa skins 

            //transperant fönster när yes och no knapparna komemr upp
            //--------------------------------------------------------------------

            //-------------------------------------------BUGS-------------------------------------------
            //  no, sedan försöker klicka på start new game igen så reagerar den endast på isbuttondown??
            //startnewgame --> yesbutton funkar inte
            // 1 frame är off när man stannar åt vännster
            //--------------------------------------------------------------------------------------


            //HUR MAN KLONAR KOD FRÅN GITHUB, MELLAN OLLIKA ENHETER

            //fn+f1, git clone, clone from github, kmr fram en lista efter man loggat in, välj fil, välj vilken mapp man vill lägga den i, ladda ner, klicka på open.
            //när man gör nått så gör man en commit så synkas den på "(>)<" den knapp skiten.


            int screenwidth = 1920;
            int screenheight = 1000;

            Raylib.SetTargetFPS(144);




            Texture2D cloudImg = Raylib.LoadTexture("cloud.png");
            Texture2D backGroundImg = Raylib.LoadTexture("himmelBakgrund.png");
            Texture2D playerMovingImg1 = Raylib.LoadTexture("playerMoving1.png");
            Texture2D playerMovingFlipImg1 = Raylib.LoadTexture("playerMovingFlip1.png");
            Texture2D playerNotMovingImg1 = Raylib.LoadTexture("playerNotMoving1.png");
            Texture2D playerNotMovingImg2 = Raylib.LoadTexture("playerNotMoving2.png");
            Texture2D playerNotMovingFlipImg1 = Raylib.LoadTexture("playerNotMovingFlip1.png");
            Texture2D playerNotMovingFlipImg2 = Raylib.LoadTexture("playerNotMovingFlip2.png");

            //introscreen variabler
            string gameState = "introScreen";



            Vector2 mousePosition = Raylib.GetMousePosition();
            //startNewGame
            Texture2D startNewGameImg = Raylib.LoadTexture("startNewGame.png");
            //yesButton
            Texture2D yesButtonImg = Raylib.LoadTexture("yesButton.png");
            Texture2D yesButtonPressedImg = Raylib.LoadTexture("yesButtonPressed.png");
            Rectangle yesButton = new Rectangle(screenwidth / 3 - 60, screenheight / 2 - 20, 190, 140);
            bool collisionYesButton = Raylib.CheckCollisionPointRec(mousePosition, yesButton);
            bool yesButtonPressed = false;
            //noButton
            Texture2D noButtonImg = Raylib.LoadTexture("noButton.png");
            Texture2D noButtonPressedImg = Raylib.LoadTexture("noButtonPressed.png");
            Rectangle noButton = new Rectangle(screenwidth / 3 + screenwidth / 3, screenheight / 2 - 20, 190, 140);
            bool collisionNoButton = Raylib.CheckCollisionPointRec(mousePosition, noButton);
            bool noButtonPressed = false;
            //newGameButton
            Texture2D newGameButtonImg = Raylib.LoadTexture("NewGameButton.png");
            Rectangle newGameButton = new Rectangle(100, 400, 440, 130);
            bool collisionNewGameButton = Raylib.CheckCollisionPointRec(mousePosition, newGameButton);
            bool newGameButtonPressed = false;




            float gravity = 6;
            float playerXpos = 100;
            float playerYpos = 250;
            float backgroundMoving = 0f;
            bool playerFlip = false;

            bool playerMoving = false;
            float playerMovingCount = 0;
            Color transparentColor = new Color(0, 0, 0, 210);



            bool mousePressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);

            int buttonTimer = 0;

            ////////


            Camera2D camera = new Camera2D();
            camera.target = new Vector2(playerXpos, playerYpos);
            camera.offset.X = playerXpos - 810;
            camera.rotation = 0f;
            camera.zoom = 1f;

            while (!Raylib.WindowShouldClose())
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //-------------------------------------------INTRO SCREEN-------------------------------------------
                ////////////////////////////////////////////////////////////////////////////////////////////////////

                if (gameState == "introScreen")
                {
                    mousePosition = Raylib.GetMousePosition();

                    collisionNewGameButton = Raylib.CheckCollisionPointRec(mousePosition, newGameButton);

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PINK);
                    Raylib.DrawText("SHUU", 100, 50, 30, Color.BLACK);
                    //newgamebutton 
                    Raylib.DrawRectangle((int)newGameButton.x, (int)newGameButton.y, (int)newGameButton.width, (int)newGameButton.height, Color.RED);
                    Raylib.DrawTextureEx(newGameButtonImg, new Vector2(100, 400), 0, 10f, Color.WHITE);

                    //-------------------------------------------START NEW GAME-------------------------------------------
                    if (collisionNewGameButton && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        newGameButtonPressed = true;
                    }
                    if (newGameButtonPressed == true)
                    {
                        collisionYesButton = Raylib.CheckCollisionPointRec(mousePosition, yesButton);
                        collisionNoButton = Raylib.CheckCollisionPointRec(mousePosition, noButton);

                        //startNewGame


                        Raylib.DrawRectangle(0, 0, 1920, 1000, transparentColor);

                        Raylib.DrawTextureEx(startNewGameImg, new Vector2(screenwidth / 2 - 310, 200), 0, 10f, Color.WHITE);

                        Raylib.DrawRectangle((int)yesButton.x, (int)yesButton.y, (int)yesButton.width, (int)yesButton.height, Color.RED);
                        Raylib.DrawRectangle((int)noButton.x, (int)noButton.y, (int)noButton.width, (int)noButton.height, Color.RED);

                        //-------------------------------------------YES BUTTON-------------------------------------------
                        //starta spelet från intoscreen genom att trycka på YES-knappen

                        if (collisionYesButton)
                        {
                            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                yesButtonPressed = true;
                                Raylib.DrawTextureEx(yesButtonPressedImg, new Vector2(screenwidth / 3 - 60, screenheight / 2), 0, 10f, Color.WHITE);
                            }

                            else
                            {
                                Raylib.DrawTextureEx(yesButtonImg, new Vector2(screenwidth / 3 - 60, screenheight / 2 - 20), 0, 10f, Color.WHITE);
                            }
                        }

                        else
                        {
                            Raylib.DrawTextureEx(yesButtonImg, new Vector2(screenwidth / 3 - 60, screenheight / 2 - 20), 0, 10f, Color.WHITE);
                        }

                        if (Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON) && yesButtonPressed == true)
                        {
                            buttonTimer += 1;

                            if (buttonTimer >= 30)
                            {
                                yesButtonPressed = false;
                                newGameButtonPressed = false;
                                buttonTimer = 0;
                                gameState = "inGame";
                            }

                        }
                        //-------------------------------------------NO BUTTON-------------------------------------------


                        if (collisionNoButton)
                        {
                            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                noButtonPressed = true;
                                Raylib.DrawTextureEx(noButtonPressedImg, new Vector2(screenwidth / 3 + screenwidth / 3, screenheight / 2), 0, 10f, Color.WHITE);
                            }

                            else
                            {
                                Raylib.DrawTextureEx(noButtonImg, new Vector2(screenwidth / 3 + screenwidth / 3, screenheight / 2 - 20), 0, 10f, Color.WHITE);
                            }
                        }

                        else
                        {
                            Raylib.DrawTextureEx(noButtonImg, new Vector2(screenwidth / 3 + screenwidth / 3, screenheight / 2 - 20), 0, 10f, Color.WHITE);
                        }

                        if (Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON) && noButtonPressed == true)
                        {
                            buttonTimer += 1;

                            if (buttonTimer >= 20)
                            {
                                noButtonPressed = false;
                                newGameButtonPressed = false;

                            }
                        }

                    }
                    Raylib.EndDrawing();
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////
                //-------------------------------------------IN GAME-------------------------------------------
                ///////////////////////////////////////////////////////////////////////////////////////////////

                //  Raylib.BeginMode2D(camera);

                if (gameState == "inGame")
                {
                    //-------------------------------------------IN GAME LOGIC-------------------------------------------
                    camera.target = new Vector2(playerXpos, playerYpos);
                    camera.offset.X = playerXpos - 810;
                    //Gravity
                    playerYpos += gravity;

                    //backgroundMoving -= 0.02f;

                    //-------------------------------------------BACKGROUND-------------------------------------------

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    //rita ut bakgrunden 
                    Raylib.DrawTextureEx(backGroundImg, new Vector2(backgroundMoving, 0), 0, 10f, Color.WHITE);
                    Raylib.DrawTextureEx(backGroundImg, new Vector2(backGroundImg.width * 10 + backgroundMoving, 0), 0, 10f, Color.WHITE);
                    //rita ut marken
                    Raylib.DrawRectangle(0, 800, 1920, 200, Color.GREEN);

                    //få bakgrunden att röra på sig i x-led
                    if (backgroundMoving <= -backGroundImg.width * 10)
                    {
                        backgroundMoving = 0f;
                    }
                    //-------------------------------------------PLAYER NOT MOVING-------------------------------------------


                    //rita ut spelaren när den står stilla och är flippad

                    //-------------------------------------------PLAYER MOVEMENT-------------------------------------------

                    //få spelaren att röra sig åt vänster och rita ut en flippad bild av spelaren
                    (bool pMoving, bool pFlip, float pX, float bgMoving) result = PlayerMovement(playerMoving, playerFlip, playerXpos, backgroundMoving);

                    playerMoving = result.pMoving;
                    playerFlip = result.pFlip;
                    playerXpos = result.pX;
                    backgroundMoving = result.bgMoving;

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)))
                    {
                        Raylib.DrawTextureEx(playerMovingFlipImg1, new Vector2(playerXpos, playerYpos), 0.0f, 10.0f, Color.WHITE);
                    }

                    else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)))
                    {
                        Raylib.DrawTextureEx(playerMovingImg1, new Vector2(playerXpos, playerYpos), 0.0f, 10.0f, Color.WHITE);
                    }


                    if (playerMoving == false && playerFlip == true)
                    {
                        if (playerMovingCount < 50)
                        {
                            Raylib.DrawTextureEx(playerNotMovingFlipImg1, new Vector2(playerXpos, playerYpos - 10), 0, 10f, Color.WHITE);

                        }
                        else if (playerMovingCount >= 50)
                        {
                            Raylib.DrawTextureEx(playerNotMovingFlipImg2, new Vector2(playerXpos, playerYpos), 0, 10f, Color.WHITE);
                        }
                        playerMovingCount += 1f;

                        if (playerMovingCount >= 100)
                        {
                            playerMovingCount = 0;
                        }
                    }

                    else if (playerMoving == false && playerFlip == false)
                    {
                        if (playerMovingCount < 50)
                        {
                            Raylib.DrawTextureEx(playerNotMovingImg1, new Vector2(playerXpos, playerYpos - 10), 0, 10f, Color.WHITE);

                        }
                        else if (playerMovingCount >= 50)
                        {
                            Raylib.DrawTextureEx(playerNotMovingImg2, new Vector2(playerXpos, playerYpos), 0, 10f, Color.WHITE);
                        }
                        playerMovingCount += 1f;

                        if (playerMovingCount >= 100)
                        {
                            playerMovingCount = 0;
                        }

                    }





                    //varför kan man inte ha else statement här även om den inte finns mellan de tidagare if satserna
                    // else
                    // {
                    //     playerMoving = false;
                    // }



                    //-------------------------------------------PLAYER JUMP-------------------------------------------

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && playerYpos > 640) //kan man ändra detta så att det korrelerar med gravitationen eller liknande 
                    {
                        gravity = -6;
                        playerMoving = true;
                    }

                    if (gravity < 6)
                    {
                        gravity += 0.1f;
                    }
                    //-------------------------------------------PLAYER COLLISION TO GROUND-------------------------------------------

                    if (playerYpos >= 650)
                    {
                        playerYpos = 650;
                    }


                    // Raylib.EndMode2D();


                    //-------------------------------------------ENTER PAUS MENU FROM GAMESCREEN-------------------------------------------
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pauseMenu";
                    }

                    Raylib.EndDrawing();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////
                //-------------------------------------------PAUSE MENU-------------------------------------------
                //////////////////////////////////////////////////////////////////////////////////////////////////

                if (gameState == "pauseMenu")
                {

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.RED);
                    Raylib.DrawText("UR GAME IS PAUSED", 150, 100, 48, Color.BLACK);
                    Raylib.EndDrawing();
                    //-------------------------------------------ENTER GAME SCREEN FROM PAUS MENU-------------------------------------------
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "inGame";
                    }

                }

            }

        }
        static (bool, bool, float, float) PlayerMovement(bool pMoving, bool pFlip, float pX, float bgMoving)
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                pMoving = true;
                pFlip = true;
                pX -= 2f;
                bgMoving += 0.8f;


            }
            //få spelaren att röra sig åt höger 
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                pMoving = true;
                pFlip = false;
                pX += 2f;
                bgMoving -= 1f;

            }
            else
            {
                pMoving = false;
            }
            return (pMoving, pFlip, pX, bgMoving);
        }

    }
}

