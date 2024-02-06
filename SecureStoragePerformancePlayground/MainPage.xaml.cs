using System.Diagnostics;

namespace SecureStoragePerformancePlayground;

public partial class MainPage
{
    // private Stopwatch stopWatch;
    private string shortStory =
        "Once upon a time, in a quaint little village nestled between rolling hills and lush forests, there lived a young girl named Elara. Elara was known throughout the village for her kind heart and adventurous spirit. She spent her days exploring the woods, chasing butterflies, and helping her neighbors with their chores.\n\nOne crisp autumn morning, as the leaves turned golden and the air grew brisk, Elara set out on one of her usual adventures. She wandered deeper into the forest than she had ever gone before, drawn by the whispering of the wind through the trees and the promise of new discoveries.\n\nAs she ventured farther into the woods, she stumbled upon a hidden clearing bathed in dappled sunlight. At the center of the clearing stood an ancient oak tree, its branches reaching up to the sky like gnarled fingers. Curious, Elara approached the tree and noticed a small, weathered door nestled in its trunk.\n\nWith trembling hands, she pushed open the door and stepped into the darkness beyond. To her surprise, she found herself in a vast underground chamber filled with glittering crystals and strange, otherworldly plants.\n\nAs she explored the chamber, Elara came across a shimmering pool of water that seemed to glow with an ethereal light. Mesmerized, she reached out to touch the water, and as her fingertips brushed its surface, she felt a surge of power course through her veins.\n\nSuddenly, the pool began to ripple and churn, and from its depths emerged a majestic dragon with scales as black as night and eyes that glowed like molten gold.\n\n\"Who dares disturb my slumber?\" the dragon boomed, its voice echoing through the chamber.\n\nElara trembled but stood her ground. \"I am Elara, daughter of this village,\" she said, her voice steady despite her fear. \"I meant no harm. I only sought to explore.\"\n\nThe dragon regarded her with a curious gaze, its fiery breath warming the air around them. \"You have courage, young one,\" it said. \"Few would dare to venture into my domain. Tell me, what is it that you seek?\"\n\n\"I seek knowledge and adventure,\" Elara replied. \"I wish to learn the secrets of this world and beyond.\"\n\nThe dragon nodded thoughtfully. \"Very well, Elara,\" it said. \"I shall grant you your wish. But know this: knowledge comes with a price. Are you willing to pay it?\"\n\nWithout hesitation, Elara nodded. \"I am,\" she said firmly.\n\nAnd so, the dragon became Elara's teacher, guiding her through the mysteries of the universe and imparting upon her wisdom beyond her years. Together, they traveled to distant lands, unraveling ancient secrets and facing formidable challenges along the way.\n\nYears passed, and Elara grew into a wise and fearless woman, renowned throughout the land for her knowledge and courage. But no matter where her adventures took her, she never forgot the day she met the dragon in the hidden chamber beneath the ancient oak tree.\n\nFor it was there, in the heart of the forest, that she discovered the true meaning of bravery, friendship, and the limitless power of the human spirit. And though her journey had only just begun, Elara knew that with the dragon by her side, there was nothing she couldn't achieve.";

    private string jsonData =
        "{\n  \"company\": \"Acme Corporation\",\n  \"employees\": [\n    {\n      \"id\": 1,\n      \"name\": \"John Doe\",\n      \"position\": \"Software Engineer\",\n      \"department\": \"Engineering\",\n      \"salary\": 80000\n    },\n    {\n      \"id\": 2,\n      \"name\": \"Jane Smith\",\n      \"position\": \"Marketing Manager\",\n      \"department\": \"Marketing\",\n      \"salary\": 75000\n    },\n    {\n      \"id\": 3,\n      \"name\": \"Michael Johnson\",\n      \"position\": \"Financial Analyst\",\n      \"department\": \"Finance\",\n      \"salary\": 85000\n    },\n    {\n      \"id\": 4,\n      \"name\": \"Emily Brown\",\n      \"position\": \"Human Resources Coordinator\",\n      \"department\": \"HR\",\n      \"salary\": 70000\n    }\n  ],\n  \"products\": [\n    {\n      \"id\": 101,\n      \"name\": \"Widget\",\n      \"category\": \"Gadgets\",\n      \"price\": 29.99\n    },\n    {\n      \"id\": 102,\n      \"name\": \"Gizmo\",\n      \"category\": \"Electronics\",\n      \"price\": 49.99\n    },\n    {\n      \"id\": 103,\n      \"name\": \"Thingamajig\",\n      \"category\": \"Tools\",\n      \"price\": 39.99\n    }\n  ]\n}\n";

    private List<string> fetchResults = new List<string>();
    public MainPage()
    {
        InitializeComponent();
        // stopWatch = new Stopwatch();
    }

    private async void LoadValuesIntoSecureStorage(object sender, EventArgs e)
    {
        SetStatusMessageAndIndicator("Loading values into SecureStorage.\nPlease wait...", true);
        await LoadValuesIntoSecureStorage();
        SetStatusMessageAndIndicator(string.Empty, false);
    }

    private async void BeginSecureStorageTest(object? sender, EventArgs e)
    {
        SetStatusMessageAndIndicator("Reading values from SecureStorage.\nPlease wait...", true);
        await LoadValuesFromSecureStorageWithDebugging();
        SetStatusMessageAndIndicator(string.Empty, false);
    }

    private void SetStatusMessageAndIndicator(string status, bool isLoading)
    {
        StatusLabel.Text = status;
        ActivityIndicator.IsRunning = isLoading;
    }

    private async Task LoadValuesIntoSecureStorage()
    {
        await SecureStorage.Default.SetAsync("ShortStory", shortStory);
        await SecureStorage.Default.SetAsync("JsonData", jsonData);
        await SecureStorage.Default.SetAsync("SingleCharacter", "Y");
        for (var i = 0; i < 100; i++)
        {
            await SecureStorage.Default.SetAsync($"{i}", $"{i}{shortStory}");
        }
    }

    private async Task LoadValuesFromSecureStorageWithDebugging()
    {
        fetchResults = new List<string>();
        var tasks = new List<Task>();
        // tasks.Add(LoadSecureStorageDataWithTimer("ShortStory"));
        // tasks.Add(LoadSecureStorageDataWithTimer("JsonData"));
        // tasks.Add(LoadSecureStorageDataWithTimer("SingleCharacter"));
        await LoadSecureStorageDataWithTimer("ShortStory");
        await LoadSecureStorageDataWithTimer("JsonData");
        await LoadSecureStorageDataWithTimer("SingleCharacter");
        for (var i = 0; i < 100; i++)
        {
            // tasks.Add(LoadSecureStorageDataWithTimer($"{i}"));
            await LoadSecureStorageDataWithTimer($"{i}");
        }

        // await Task.WhenAll(tasks);
        ListView.ItemsSource = fetchResults;
    }

    private async Task LoadSecureStorageDataWithTimer(string key)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var value = await SecureStorage.Default.GetAsync(key);
        stopWatch.Stop();
        var result = $"Fetched {key} - Elapsed Milliseconds: {stopWatch.ElapsedMilliseconds}";
        fetchResults.Add(result);
    }
}