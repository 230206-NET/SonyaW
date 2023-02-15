namespace Hangman {

   public class FrameHolder {
        public string[] hangFrames = { 
        """   
             ____
            |    |      
            |      
            |        
            |    
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  ('-')    'Oh.. Hi?'
            |        
            |    
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (O_O)   'Wait.. What's happening here?'   
            |   | |     
            |    
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (°n°)  'Hold on, We can talk about this!'    
            |   | |     
            |   |_|
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (-_-)  'Please, let someone else guess!'    
            |  /| |     
            |   |_|
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (~n~)  'HEY MAN HURRY IT FINISHED AN ARM!'    
            |  /| |     
            | / |_|
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (;-;)  'I should've bought life insurance.'    
            |  /| |\     
            | / |_|
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (`~`)  'I can see the light...'    
            |  /| |\     
            | / |_| \
            |   
           _|_
        """,
        """   
             ____
            |    |      
            |  (T~T)  'Next time stick to minesweeper or something jeez...'    
            |  /| |\     
            | / |_| \
            |   /
           _|_
        """,
        """   
             ____
            |    |      
            |  (ToT)  'I know a cheat to win! Press CTRL+C... FAST!'    
            |  /| |\     
            | / |_| \
            |   /
           _|_ /
        """,
        """   
             ____
            |    |      
            |  (>_>)  'Didn't fall for it huh? Well, it was worth the try.'    
            |  /| |\     
            | / |_| \
            |   / \
           _|_ /
        """
        };
        public string winFrame =
        """   
                                    YOU WIN!
               ('u')  'I didn't doubt you for a second buddy!'    
               /| |\     
              / | | \
                |||
                |||
        """;

        public string loseFrame = 
        """   
             ____
            |    |                  YOU LOSE!
            |  (X_X)  '...'    
            |  /| |\     
            | / | | \
            |   / \
           _|_ /   \
        """;

    }

}