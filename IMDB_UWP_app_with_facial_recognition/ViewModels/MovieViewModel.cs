using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using IMDB_UWP_app_with_facial_recognition.Models;
using IMDB_UWP_app_with_facial_recognition.Services;
using IMDB_UWP_app_with_facial_recognition.Views;

namespace IMDB_UWP_app_with_facial_recognition.ViewModels
{
    class MovieViewModel : ViewModelBase
    {
        private Movie movie;
        private INavigationService navigationService;

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

        private static string _movieId;

        public string MovieId
        {
            get { return _movieId; }
            set { SetProperty(ref _movieId, value); }
        }

        private static string _movieTitle;

        public string MovieTitle
        {
            get { return _movieTitle; }
            set { SetProperty(ref _movieTitle, value); }
        }

        private static string _moviePoster;

        public string MoviePoster
        {
            get { return _moviePoster; }
            set { SetProperty(ref _moviePoster, value); }
        }

        private string _requestProgress = "";

        public string RequestProgress
        {
            get { return _requestProgress; }
            set { SetProperty(ref _requestProgress, value); }
        }

        public MovieViewModel(INavigationService _navigationService)
        {
            navigationService = _navigationService;
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
            RequestProgress = "trying to find the movie...";
            try
            {
                var movieDto = await movie.getMovie(movie.getMovieIdFromUrl(ImdbUrl));

                MovieId = movieDto.MovieId;
                MovieTitle = movieDto.MovieTitle;
                MoviePoster = movieDto.MoviePoster;

//                RequestProgress = "";

                RemoveCommands();

                navigationService.Navigate(typeof(CheckMoviePage));

            }
            catch (Exception e)
            {
                RequestProgress = e.InnerException.Message;
            }
        }
    }
}
