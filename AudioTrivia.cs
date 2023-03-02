using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using static Android.Views.WindowInsetsAnimation;

namespace Game_Center
{
    public class AudioTrivia
    {

        private static int press_button_sound, bad_answer_sound, correct_answer_sound,
            forgot_correct_sound, forgot_wrong_sound, register_correct_sound, register_wrong_sound, trivia_correct_sound, trivia_wrong_sound;
        private static SoundPool press_button_pool, bad_answer_pool, correct_answer_pool,
            forgot_correct_pool, forgot_wrong_pool, register_correct_pool, register_wrong_pool, trivia_correct_pool, trivia_wrong_pool;

        [Obsolete]
        public AudioTrivia(Android.Content.Context context)
        {

            press_button_pool = new SoundPool(1, Stream.Music, 1);
            press_button_sound = press_button_pool.Load(context, Resource.Raw.press_button, 1);

            bad_answer_pool = new SoundPool(1, Stream.Music, 1);
            bad_answer_sound = bad_answer_pool.Load(context, Resource.Raw.awnser_bad, 1);

            correct_answer_pool = new SoundPool(1, Stream.Music, 1);
            correct_answer_sound = correct_answer_pool.Load(context, Resource.Raw.Correct_Answer_Sound, 1);

            forgot_correct_pool = new SoundPool(1, Stream.Music, 1);
            forgot_correct_sound = forgot_correct_pool.Load(context, Resource.Raw.forgot_correct, 1);

            forgot_wrong_pool = new SoundPool(1, Stream.Music, 1);
            forgot_wrong_sound = forgot_wrong_pool.Load(context, Resource.Raw.forgot_wrong, 1);

            register_correct_pool = new SoundPool(1, Stream.Music, 1);
            register_correct_sound = register_correct_pool.Load(context, Resource.Raw.register_correct, 1);

            register_wrong_pool = new SoundPool(1, Stream.Music, 1);
            register_wrong_sound = register_wrong_pool.Load(context, Resource.Raw.register_wrong, 1);

            trivia_correct_pool = new SoundPool(1, Stream.Music, 1);
            trivia_correct_sound = trivia_correct_pool.Load(context, Resource.Raw.trivia_correct, 1);

            trivia_wrong_pool = new SoundPool(1, Stream.Music, 1);
            trivia_wrong_sound = trivia_wrong_pool.Load(context, Resource.Raw.trivia_wrong, 1);
        }


        public void TriviaCorrectSound()
        {

            trivia_correct_pool.Play(trivia_correct_sound, 1, 1, 1, 0, 1);
        }

        public void TriviawrongSound()
        {

            trivia_wrong_pool.Play(trivia_wrong_sound, 1, 1, 1, 0, 1);
        }

        public void RegisterCorrectSound()
        {

            register_correct_pool.Play(register_correct_sound, 1, 1, 1, 0, 1);
        }
        public void RegisterWrongSound()
        {

            register_wrong_pool.Play(register_wrong_sound, 1, 1, 1, 0, 1);
        }

        public void ForgotPassWrongSound()
        {

            forgot_wrong_pool.Play(forgot_wrong_sound, 1, 1, 1, 0, 1);
        }

        public void ForgotPassCorrectSound()
        {

            forgot_correct_pool.Play(forgot_correct_sound, 1, 1, 1, 0, 1);
        }
        public void GoodAnswerSound()
        {

            correct_answer_pool.Play(correct_answer_sound, 1, 1, 1, 0, 1);
        }
        public void BadAnswerSound()
        {
           
            bad_answer_pool.Play(bad_answer_sound, 1, 1, 1, 0, 1);
        }

        public void pressButton()
        {

            press_button_pool.Play(press_button_sound, 1, 1, 1, 0, 1);
        }


    }
}