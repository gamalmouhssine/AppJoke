using AppJoke.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppJoke;

public partial class MainPage : ContentPage
{
    private HttpClient _httpClient;
    private ObservableCollection<Joke> _jokes;
    private string _selectedCategory = "Any";
    private string[] _categories = { "Any", "Dark", "Pun", "Misc", "Programming" };

    public MainPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _jokes = new ObservableCollection<Joke>();
        BindingContext = this;
    }

    public ObservableCollection<Joke> Jokes => _jokes;
    public string[] Categories => _categories;
    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FetchJokesAsync();
            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await FetchJokesAsync();
    }

    private async Task FetchJokesAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<JokeApiResponse>($"https://v2.jokeapi.dev/joke/{SelectedCategory}?type=twopart&amount=10");
            _jokes.Clear();
            foreach (var joke in response.Jokes)
            {
                _jokes.Add(joke);
                await AnimateJokeEntry(_jokes.Count - 1);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to fetch jokes: {ex.Message}", "OK");
        }
    }

    private async Task AnimateJokeEntry(int index)
    {
        var jokeView = GetJokeView(index);
        if (jokeView != null)
        {
            jokeView.Opacity = 0;
            jokeView.TranslationY = 50;
            await Task.WhenAll(
                jokeView.FadeTo(1, 500),
                jokeView.TranslateTo(0, 0, 500, Easing.SpringOut)
            );
        }
    }

    private View GetJokeView(int index)
    {
        var collectionView = this.FindByName<CollectionView>("JokesCollectionView");
        var container = collectionView?.GetVisualTreeDescendants().OfType<ScrollView>().FirstOrDefault();
        var jokeView = container?.GetVisualTreeDescendants().ElementAtOrDefault(index) as View;
        return jokeView;
    }

    private void OnReactionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Joke joke)
        {
            string reaction = button.Text;
            Console.WriteLine($"User reacted with {reaction} to joke ID {joke.Id}");
            AnimateReaction(button);
        }
    }

    private async void AnimateReaction(Button button)
    {
        await button.ScaleTo(1.2, 100);
        await button.ScaleTo(1, 100);
    }
}