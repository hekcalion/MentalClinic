namespace MentalClinic.API.Models.Domain;

public class Test
{
    public string id { get; set; }

    public string BlockHeader { get; set; }

    public string BlockSubHeader { get; set; }

    public string Name { get; set; }

    public string ShortDescription { get; set; }

    public string long_test_description { get; set; }

    public List<Question> Questions { get; set; }

    public List<Result> Result { get; set; }
}


public class Question
{
    public string question_id { get; set; }

    public string question_text { get; set; }

    public List<AnswerOptions> answer_options { get; set; }
}


public class AnswerOptions
{
    public string Text {  get; set; }

    public int Value { get; set; }
}


public class Result
{
    public int MinTotalValue { get; set; }

    public int MaxTotalValue { get; set; }

    public string ResultText { get; set; }
}
