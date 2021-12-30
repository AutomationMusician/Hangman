using System;
using System.IO;
using System.Collections.Generic;

namespace Hangman
{
    enum CharState {
        NotGuessed,
        CorrectlyGuessed,
        IncorrectlyGuessed
    }

    enum GuessResponse {
        None,
        Forbiden,
        Invalid,
        AlreadyGuessed,
        Correct,
        Incorrect
    }

    enum GameStatus {
        Playing,
        Won,
        Lost
    }
}
