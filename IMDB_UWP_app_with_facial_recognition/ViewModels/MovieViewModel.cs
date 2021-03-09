using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IMDB_UWP_app_with_facial_recognition.Models;

namespace IMDB_UWP_app_with_facial_recognition.ViewModels
{
    class MovieViewModel : ViewModelBase
    {
        private Movie movie;

        private string _example;

        public string Example
        {
            get { return _example; }
            set { SetProperty(ref _example, value); }
        }

        private string _imdbUrl = "https://www.imdb.com/title/tt0120737/";

        public string ImdbUrl
        {
            get { return _imdbUrl; }
            set { SetProperty(ref _imdbUrl, value); }
        }

        private string _isSomethingReturned = "no";

        public string IsSomethingReturned
        {
            get { return _isSomethingReturned; }
            set { SetProperty(ref _isSomethingReturned, value); }
        }

        public MovieViewModel()
        {
            movie = new Movie();
            CreateGetMovieCommand();
        }

        public ICommand GetMovieCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteGetMovieCommand()
        {
            return true;
        }

        private void CreateGetMovieCommand()
        {
            GetMovieCommand = new RelayCommand(GetMovieExecute, CanExecuteGetMovieCommand);
        }

        public async void GetMovieExecute()
        {
            try
            {
                var movieDto = await movie.getMovie(movie.getMovieIdFromUrl(ImdbUrl));
//                if (movieDto.MovieId != null)
//                {
//                    IsSomethingReturned = $"YESSS {movieDto.MovieId}";
//                }

                IsSomethingReturned = "uhhhhh";
            }
            catch (Exception e)
            {
                IsSomethingReturned = "error";
            }

            
        }
    }
}
