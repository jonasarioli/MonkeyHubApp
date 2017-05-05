using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {               
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if(SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearchCommand { get; }

        public Command AboutCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }

        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
            Resultados = new ObservableCollection<Tag>();
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
        }

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_monkeyHubApiService, tag);
        }

        async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        async void ExecuteSearchCommand(object obj)
        {
            await Task.Delay(2000);
            Debug.WriteLine($"Clicou no botão! {DateTime.Now}");
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Voce pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if(resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado.", "OK");
                
                var tagsRetornadasDoServico = await _monkeyHubApiService.GetTagsAsync();

                Resultados.Clear();
                if (tagsRetornadasDoServico != null)
                {
                    foreach (var tag in tagsRetornadasDoServico)
                    {
                        Resultados.Add(tag);
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "De nada.", "OK");
                Resultados.Clear();
            }
            
        }

        bool CanExecuteSearchCommand(object arg)
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }
    }
}
