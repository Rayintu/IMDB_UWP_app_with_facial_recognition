using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using IMDB_UWP_app_with_facial_recognition.DTOs;
using IMDB_UWP_app_with_facial_recognition.Models;
using IMDB_UWP_app_with_facial_recognition.Services;
using IMDB_UWP_app_with_facial_recognition.Validators;
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

        private static string _imdbUrl;

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

        private string _movieSynopsis;

        public string MovieSynopsis
        {
            get { return _movieSynopsis; }
            set { SetProperty(ref _movieSynopsis, value); }
        }

        private string _movieRating;

        public string MovieRating
        {
            get { return _movieRating; }
            set { SetProperty(ref _movieRating, value); }
        }

        private string _movieYear;

        public string MovieYear
        {
            get { return _movieYear; }
            set { SetProperty(ref _movieYear, value); }
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
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
            CreatePageBackCommand();
            CreateGoToHomeCommand();
            CreateGoToMovieDetailsCommand();
            CreateGetMovieDetailsCommand();
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
                if (IMDBValidator.ValidateImdbUrl(ImdbUrl))
                {
                    var movieDto = await movie.getMovie(movie.getMovieIdFromUrl(ImdbUrl));

                    MovieId = movieDto.MovieId;
                    MovieTitle = movieDto.MovieTitle;
                    MoviePoster = movieDto.MoviePoster;

                    RequestProgress = "";

                    RemoveCommands();

                    navigationService.Navigate(typeof(CheckMoviePage));
                }
                else
                {
                    RequestProgress =
                        "Please enter a valid imdb url! It should look something like this: https://www.imdb.com/title/ttxxxxxxx/";
                }

            }
            catch (HttpRequestException e)
            {
                RequestProgress = e.Message;
            }
        }

        public ICommand PageBackCommand
        {
            get;
            internal set;
        }

        private bool CanExecutePageBackCommand()
        {
            return true;
        }

        private void CreatePageBackCommand()
        {
            PageBackCommand = new RelayCommand(PageBackExecute, CanExecutePageBackCommand);
        }

        public void PageBackExecute()
        {
            RemoveCommands();
            navigationService.GoBack();
        }

        public ICommand GoToHomeCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteGoToHomeCommand()
        {
            return true;
        }

        private void CreateGoToHomeCommand()
        {
            GoToHomeCommand = new RelayCommand(GoToHomeExecute, CanExecuteGoToHomeCommand);
        }

        public void GoToHomeExecute()
        {
            RemoveCommands();
            navigationService.Navigate(typeof(MainPage));
        }

        public ICommand GoToMovieDetailsCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteGoToMovieDetailsCommand()
        {
            return MovieId != null;
        }

        private void CreateGoToMovieDetailsCommand()
        {
            GoToMovieDetailsCommand = new RelayCommand(GoToMovieDetailsCommandExecute, CanExecuteGoToMovieDetailsCommand);
        }

        public void GoToMovieDetailsCommandExecute()
        {
            RemoveCommands();
            navigationService.Navigate(typeof(MovieDetailsPage));
        }

        public ICommand GetMovieDetailsCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteGetMovieDetailsCommand()
        {
            return MovieId != null;
        }

        private void CreateGetMovieDetailsCommand()
        {
            GetMovieDetailsCommand = new RelayCommand(GetMovieDetailsExecute, CanExecuteGetMovieDetailsCommand);
        }

        public async void GetMovieDetailsExecute()
        {
            RequestProgress = "Getting movie details...";
            try
            {
                var movieDetailsDto = await movie.getMovieDetails(movie.getMovieIdFromUrl(ImdbUrl));

                MovieId = movieDetailsDto.MovieId;
                MovieTitle = movieDetailsDto.MovieTitle;
                MoviePoster = movieDetailsDto.MoviePoster;
                Source = movieDetailsDto.Source;
                MovieSynopsis = movieDetailsDto.MovieSynopsis;
                MovieRating = movieDetailsDto.MovieRating;
                MovieYear = ParserService.getYearFromReleaseDate(movieDetailsDto.DatePublished);

                RequestProgress = "";
            }
            catch (Exception e)
            {
                RequestProgress = e.Message;
            }
        }

    }
}
