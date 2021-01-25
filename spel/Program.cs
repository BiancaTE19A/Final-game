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
            //INITIATE VALUES
            int screenWidth = 1920;
            int screenHeight = 1000;
            Raylib.InitWindow(screenWidth, screenHeight, "Mitt Spel");
            Raylib.SetTargetFPS(144);

            //IMAGE VALUES
            //Background
            Texture2D cloudImg = Raylib.LoadTexture("cloud.png");
            Texture2D backGroundImg = Raylib.LoadTexture("himmelBakgrund.png");
            Texture2D tunnelImg = Raylib.LoadTexture("tunnel.png");
            //Player
            Texture2D playerMovingImg1 = Raylib.LoadTexture("playerMoving1.png");
            Texture2D playerMovingFlipImg1 = Raylib.LoadTexture("playerMovingFlip1.png");
            Texture2D playerNotMovingImg1 = Raylib.LoadTexture("playerNotMoving1.png");
            Texture2D playerNotMovingImg2 = Raylib.LoadTexture("playerNotMoving2.png");
            Texture2D playerNotMovingFlipImg1 = Raylib.LoadTexture("playerNotMovingFlip1.png");
            Texture2D playerNotMovingFlipImg2 = Raylib.LoadTexture("playerNotMovingFlip2.png");
            //Buttons
            Texture2D startNewGameImg = Raylib.LoadTexture("startNewGame.png");
            Texture2D yesButtonImg = Raylib.LoadTexture("yesButton.png");
            Texture2D yesButtonPressedImg = Raylib.LoadTexture("yesButtonPressed.png");
            Texture2D noButtonImg = Raylib.LoadTexture("noButton.png");
            Texture2D noButtonPressedImg = Raylib.LoadTexture("noButtonPressed.png");
            Texture2D newGameButtonImg = Raylib.LoadTexture("NewGameButton.png");

            //GAME VALUES
            float backgroundMoving = 0f;
            bool isGrounded = true;
            string gameState = "game";
            Vector2 mousePosition = Raylib.GetMousePosition();
            bool mousePressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);

            //BUTTON VALUES
            Rectangle yesButton = new Rectangle(screenWidth / 3 - 60, screenHeight / 2 - 20, 190, 140);
            Rectangle noButton = new Rectangle(screenWidth / 3 + screenWidth / 3, screenHeight / 2 - 20, 190, 140);
            Rectangle newGameButton = new Rectangle(100, 400, 440, 130);
            bool collisionYesButton = Raylib.CheckCollisionPointRec(mousePosition, yesButton);
            bool yesButtonPressed = false;
            bool collisionNoButton = Raylib.CheckCollisionPointRec(mousePosition, noButton);
            bool noButtonPressed = false;
            bool collisionNewGameButton = Raylib.CheckCollisionPointRec(mousePosition, newGameButton);
            bool newGameButtonPressed = false;
            int buttonTimer = 0;

            //PLAYER VALUES
            float gravity = 10;
            float playerXpos = 100;
            float playerYpos = 800;
            bool playerFlip = false;
            bool playerMoving = false;
            float playerMovingCount = 0;
            //player size
            Rectangle player = new Rectangle((int)playerXpos, (int)playerYpos - 10, 120, 160);

            //ENEMY values
            // float enemyXpos = 1400;
            // float enemyYpos = 650;
            // float enemySpeed = 0.8f;

            //SPIKES VALUES
            Rectangle spikes = new Rectangle(300, 630, 50, 150);

            //COLOR VALUES
            Color transparentColor = new Color(0, 0, 0, 210);

            //MENU VALUES
            bool isPaused = false;

            //HEALTH VALUES
            float playerHealth = 180;
            float healthBarWidth = 0;

            while (!Raylib.WindowShouldClose())
            {
                //INTRO STATE
                if (gameState == "intro")
                {

                    mousePosition = Raylib.GetMousePosition();
                    collisionNewGameButton = Raylib.CheckCollisionPointRec(mousePosition, newGameButton);
                    //GRAFIK
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PINK);
                    Raylib.DrawText("SHUU", 100, 50, 30, Color.BLACK);
                    //NEW GAME BUTTON
                    Raylib.DrawRectangle((int)newGameButton.x, (int)newGameButton.y, (int)newGameButton.width, (int)newGameButton.height, Color.RED);
                    Raylib.DrawTextureEx(newGameButtonImg, new Vector2(100, 400), 0, 10f, Color.WHITE);

                    if (collisionNewGameButton && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        newGameButtonPressed = true;
                    }
                    if (newGameButtonPressed == true)
                    {
                        collisionYesButton = Raylib.CheckCollisionPointRec(mousePosition, yesButton);
                        collisionNoButton = Raylib.CheckCollisionPointRec(mousePosition, noButton);

                        Raylib.DrawRectangle(0, 0, 1920, 1000, transparentColor);
                        Raylib.DrawTextureEx(startNewGameImg, new Vector2(screenWidth / 2 - 310, 200), 0, 10f, Color.WHITE);

                        Raylib.DrawRectangle((int)yesButton.x, (int)yesButton.y, (int)yesButton.width, (int)yesButton.height, Color.RED);
                        Raylib.DrawRectangle((int)noButton.x, (int)noButton.y, (int)noButton.width, (int)noButton.height, Color.RED);

                        //KOLLA OM SPELAREN STARTAR SPELET
                        if (collisionYesButton)
                        {
                            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                yesButtonPressed = true;
                                Raylib.DrawTextureEx(yesButtonPressedImg, new Vector2(screenWidth / 3 - 60, screenHeight / 2), 0, 10f, Color.WHITE);
                            }

                            else
                            {
                                Raylib.DrawTextureEx(yesButtonImg, new Vector2(screenWidth / 3 - 60, screenHeight / 2 - 20), 0, 10f, Color.WHITE);
                            }
                        }

                        else
                        {
                            Raylib.DrawTextureEx(yesButtonImg, new Vector2(screenWidth / 3 - 60, screenHeight / 2 - 20), 0, 10f, Color.WHITE);
                        }

                        if (Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON) && yesButtonPressed == true)
                        {
                            buttonTimer += 1;

                            if (buttonTimer >= 30)
                            {

                                yesButtonPressed = false;
                                newGameButtonPressed = false;
                                buttonTimer = 0;
                                gameState = "game";
                            }

                        }

                        if (collisionNoButton)
                        {
                            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                noButtonPressed = true;
                                Raylib.DrawTextureEx(noButtonPressedImg, new Vector2(screenWidth / 3 + screenWidth / 3, screenHeight / 2), 0, 10f, Color.WHITE);
                            }

                            else
                            {
                                Raylib.DrawTextureEx(noButtonImg, new Vector2(screenWidth / 3 + screenWidth / 3, screenHeight / 2 - 20), 0, 10f, Color.WHITE);
                            }
                        }

                        else
                        {
                            Raylib.DrawTextureEx(noButtonImg, new Vector2(screenWidth / 3 + screenWidth / 3, screenHeight / 2 - 20), 0, 10f, Color.WHITE);
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

                if (gameState == "game")
                {


                    //GRAVITY
                    playerYpos += gravity;

                    //PLAYER MOVEMENT METOD
                    (bool pMoving, bool pFlip, float pX, float bgMoving) result = PlayerMovement(playerMoving, playerFlip, playerXpos, backgroundMoving);

                    playerMoving = result.pMoving;
                    playerFlip = result.pFlip;
                    playerXpos = result.pX;
                    backgroundMoving = result.bgMoving;

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    //DRAW BACKGROUND
                    Raylib.DrawTextureEx(backGroundImg, new Vector2(0, 0), 0f, 10f, Color.WHITE);
                    //DRAW GROUND
                    Raylib.DrawRectangle(0, 900, 1920, 100, Color.GREEN);
                    //DRAW PLATFORMS
                    Raylib.DrawRectangle(0, 605, 250, 50, Color.RED);
                    Raylib.DrawRectangle(250, 605, 50, 200, Color.GREEN);
                    Raylib.DrawRectangle(500, 780, 1000, 200, Color.PURPLE);
                    Raylib.DrawRectangle(1450, 200, 50, 580, Color.LIME);
                    Raylib.DrawRectangle(650, 500, 800, 50, Color.YELLOW);
                    Raylib.DrawRectangle(0, 365, 400, 50, Color.PINK);
                    Raylib.DrawRectangle(200, 180, 1720, 20, Color.BLUE);

                    //DRAW TUNNEL
                    Raylib.DrawTextureEx(tunnelImg, new Vector2(1800, 5), 0.0f, 10f, Color.WHITE);


                    //DRAW PLAYER RECTANGLE
                    player = new Rectangle((int)playerXpos, (int)playerYpos - 10, 120, 160);
                    Raylib.DrawRectangleRec(player, Color.YELLOW);

                    //SCREEN COLLISION
                    if (playerXpos <= 0)
                    {
                        playerXpos = 0;
                    }
                    if (playerXpos >= 1800)
                    {
                        playerXpos = 1800;
                    }
                    if (playerYpos <= 0)
                    {
                        playerYpos = 0;
                        gravity = 0;
                    }


                    //PLATFORM COLLISION
                    //player-width = 120 | player-height = 150
                    if (playerXpos > 380 && playerXpos < 390 && playerYpos > 590 && playerYpos < 900)
                    {
                        //checks purple platform collision: left
                        playerXpos = 380;
                    }

                    if (playerXpos > 380 && playerXpos <= 1500 && playerYpos > 630 && playerYpos < 680)
                    {
                        //checks purple platform collision: up
                        isGrounded = true;
                        playerYpos = 630;

                    }
                    if (playerXpos > 530 && playerXpos <= 1500 && playerYpos > 500 && playerYpos < 560)
                    {
                        //checks yellow platform collision: down
                        playerYpos = 560;
                        gravity = 0;
                    }
                    if (playerXpos > 530 && playerXpos <= 1500 && playerYpos > 350 && playerYpos < 400)
                    {
                        //checks yellow platform collision: up
                        playerYpos = 350;
                        isGrounded = true;
                    }
                    if (playerXpos > 530 && playerXpos < 1500 && playerYpos > 500 && playerYpos < 560)
                    {
                        //checks yellow platform collision: left
                        playerXpos = 530;
                    }
                    if (playerXpos >= 0 && playerXpos < 270 && playerYpos > 455 && playerYpos < 505)
                    {
                        //checks red and green platform collision: up
                        playerYpos = 455;
                        isGrounded = true;
                    }
                    if (playerXpos >= 0 && playerXpos <= 130 && playerYpos > 605 && playerYpos < 655)
                    {
                        //checks red platform collision: down
                        playerYpos = 655;
                        gravity = 0;
                    }
                    if (playerXpos > 130 && playerXpos < 270 && playerYpos > 755 && playerYpos <= 805)
                    {
                        //checks green platform collision: down
                        playerYpos = 805;
                        gravity = 0;
                    }
                    if (playerXpos >= 130 && playerXpos < 180 && playerYpos > 655 && playerYpos <= 805)
                    {
                        //checks green platform collision: left
                        playerXpos = 130;
                        //GLITCH I HÖRNET
                    }

                    if (playerXpos >= 0 && playerXpos < 400 && playerYpos > 215 && playerYpos <= 265)
                    {
                        //checks pink platform collision: up
                        playerYpos = 215;
                        isGrounded = true;
                    }
                    if (playerXpos >= 0 && playerXpos < 400 && playerYpos > 375 && playerYpos <= 425)
                    {
                        //checks pink platform collision: down
                        playerYpos = 425;
                        gravity = 0;
                    }
                    if (playerXpos >= 80 && playerXpos <= 1920 && playerYpos > 30 && playerYpos <= 50)
                    {
                        //checks blue platform collision: up
                        playerYpos = 30;
                        isGrounded = true;
                    }
                    if (playerXpos >= 80 && playerXpos < 100 && playerYpos > 30 && playerYpos <= 200)
                    {
                        //checks blue platform collision: left
                        playerXpos = 80;
                    }
                    if (playerXpos >= 80 && playerXpos <= 1920 && playerYpos > 180 && playerYpos <= 200)
                    {
                        //checks blue platform collision: down
                        playerYpos = 200;
                        gravity = 0;
                    }

                    Raylib.DrawRectangleRec(spikes, Color.BLACK);
                    if (Raylib.CheckCollisionRecs(player, spikes))
                    {
                        gameState = "dead";
                    }

                    //DRAW SPIKES1
                    Raylib.DrawTriangle(new Vector2(300, 605), new Vector2(300, 655), new Vector2(350, 630), Color.BLUE);
                    Raylib.DrawTriangle(new Vector2(300, 655), new Vector2(300, 705), new Vector2(350, 680), Color.BLUE);
                    Raylib.DrawTriangle(new Vector2(300, 705), new Vector2(300, 755), new Vector2(350, 730), Color.BLUE);
                    Raylib.DrawTriangle(new Vector2(300, 755), new Vector2(300, 805), new Vector2(350, 780), Color.BLUE);
                    //DRAW SPIKES2
                    // Raylib.DrawTriangle(new Vector2(600, 775), new Vector2(650, 800), new Vector2(650, 750), Color.BLACK);
                    // Raylib.DrawTriangle(new Vector2(600, 825), new Vector2(650, 850), new Vector2(650, 800), Color.BLACK);
                    // Raylib.DrawTriangle(new Vector2(600, 875), new Vector2(650, 900), new Vector2(650, 850), Color.BLACK);
                    // Raylib.DrawTriangle(new Vector2(600, 925), new Vector2(650, 950), new Vector2(650, 900), Color.BLACK);


                    //DEBUG FUNCTION
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                    {
                        playerHealth -= 20;
                    }
                    Raylib.DrawText("X: " + playerXpos + " Y: " + playerYpos, 100, 100, 32, Color.BLACK);


                    //PLAYER MOVEMENT
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)))
                    {
                        Raylib.DrawTextureEx(playerMovingFlipImg1, new Vector2(playerXpos, playerYpos), 0.0f, 10.0f, Color.WHITE);
                        // Raylib.DrawRectangle((int)playerXpos, (int)playerYpos, playerMovingFlipImg1.width * 10, playerMovingFlipImg1.height * 10, Color.RED);
                    }

                    else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)))
                    {
                        Raylib.DrawTextureEx(playerMovingImg1, new Vector2(playerXpos, playerYpos), 0.0f, 10.0f, Color.WHITE);
                        // Raylib.DrawRectangle((int)playerXpos, (int)playerYpos, playerMovingFlipImg1.width * 10, playerMovingFlipImg1.height * 10, Color.RED);
                    }

                    //PLAYER IDLE (LEFT)
                    if (playerMoving == false && playerFlip == true)
                    {
                        if (isGrounded == true)
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
                        //JUMP
                        else if (isGrounded == false)
                        {
                            Raylib.DrawTextureEx(playerNotMovingFlipImg1, new Vector2(playerXpos, playerYpos - 10), 0, 10f, Color.WHITE);
                        }
                    }

                    //PLAYER IDLE (RIGHT)
                    else if (playerMoving == false && playerFlip == false)
                    {
                        if (isGrounded == true)
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
                        //JUMP
                        else if (isGrounded == false)
                        {
                            Raylib.DrawTextureEx(playerNotMovingImg1, new Vector2(playerXpos, playerYpos - 10), 0, 10f, Color.WHITE);
                        }
                    }

                    //PLAYER JUMP
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && isGrounded == true)
                    {
                        gravity = -10;
                        playerMoving = true;
                        isGrounded = false;
                    }

                    if (gravity < 10)
                    {
                        gravity += 0.15f;

                    }

                    //PLAYER COLLISION TO GROUND
                    if (playerYpos >= 820)
                    {
                        playerYpos = 820;
                        isGrounded = true;
                    }
                    else if (playerYpos < 820)
                    {
                        isGrounded = false;
                    }

                    //PLAYER HEALTH
                    healthBarWidth = playerHealth;
                    Raylib.DrawRectangle((int)playerXpos - 20, (int)playerYpos - 50, (int)healthBarWidth, 20, Color.RED);


                    //ENEMY MOVEMENT
                    // if (enemyXpos <= 1200)
                    // {
                    //     enemySpeed = 0.8f;
                    // }
                    // else if (enemyXpos >= 1700)
                    // {
                    //     enemySpeed = -0.8f;
                    // }
                    // enemyXpos += enemySpeed;

                    // //RITA FIENDE
                    // Raylib.DrawCircle((int)enemyXpos - 10, (int)enemyYpos - 10, 20, Color.BLUE);


                    //KOLLA OM SPELAREN FÅR PAUSA SPELET
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB) && isPaused == false)
                    {
                        isPaused = true;
                        gameState = "pause";
                    }
                    //BYT PAUSE BOOL
                    if (isPaused == true)
                    {
                        isPaused = false;
                    }


                    Raylib.EndDrawing();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////
                //-------------------------------------------PAUSE MENU-------------------------------------------
                //////////////////////////////////////////////////////////////////////////////////////////////////

                if (gameState == "pause")
                {

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.RED);
                    Raylib.DrawText("UR GAME IS PAUSED", 150, 100, 48, Color.BLACK);
                    Raylib.EndDrawing();
                    //-------------------------------------------ENTER GAME SCREEN FROM PAUS MENU-------------------------------------------
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB) && isPaused == false)
                    {
                        isPaused = true;
                        gameState = "game";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB) && isPaused == true)
                    {
                        isPaused = false;
                    }

                }
                if (gameState == "dead")
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.RED);
                    Raylib.DrawText("You are dead", 200, 200, 100, Color.BLACK);
                    Raylib.EndDrawing();


                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        playerXpos = 100;
                        playerYpos = 850;
                        playerFlip = false;
                        gameState = "game";
                    }



                    // if(menuTarget == 1)
                    // {
                    //     menuColor = Color.GRAY;
                    // }
                    // else
                    // {
                    //     menuColor = Color.WHITE;
                    // }
                }


            }

        }
        static (bool, bool, float, float) PlayerMovement(bool pMoving, bool pFlip, float pX, float bgMoving)
        {
            //PLAYER SPEED
            //få spelaren att röra sig åt vänster
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                pMoving = true;
                pFlip = true;
                pX -= 5f;
                bgMoving += 1f;
            }
            //få spelaren att röra sig åt höger 
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                pMoving = true;
                pFlip = false;
                pX += 5f;
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





