var Elita = Elita || {};

Elita.QuestionSet = (function ($, ko) {
    var returnFunction = "return",
        questionSetForm = $("#questionSetForm"),
        questionSetViewModel = {

            save: function () {
                //$("#questionSetForm").validate();
                var data = ko.toJSON(questionSetViewModel.QuestionSet);
                debugger;
                var newData = { questionSet: data };
                var nda = "\"" + data + "\"";


                $.ajax({
                    type: "POST",
                    url: "QuestionSetForm.aspx/SaveQuestionSet",
                    data: nda,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        debugger;
                    }
                });


                debugger;
                alert(data);

            }
        }
    function init(model) {
        // Merge any properties from the model being passed to the view
        // with the existing view model.

        var questionModel = function (data) {
            var self = this;

            ko.mapping.fromJS(data, {}, self);

            self.DisplayScalePrecision = ko.computed(function () {
                return (self.AnswerType() == "4");
            }, self);

            self.DisplayLength = ko.computed(function () {
                return (self.AnswerType() == "2");
            }, self);

            self.DisplayAnswers = ko.computed(function () {
                return ((self.AnswerType() == "0") || (self.AnswerType() == "3"));
            }, self);

            self.AddAnswer = function (parent) {
                debugger;
                self.Answers().push({
                    Id: null,
                    Code: "",
                    Text: "",
                    UIProgCode: ""
                });
            };

            self.AddPreCondition = function (parent) {
                // debugger;
                self.PreConditions.push({
                    Id: null,
                    QsCode: "",
                    AnsCode: ""
                });
            };

            self.DeleteAnswer = function (answer) {
                // debugger;
                self.Answers.remove(answer);

            };

            self.DeletePreCondition = function (precondition) {
                //debugger;
                self.PreConditions.remove(precondition);
            };

            self.Move = function(positions, parent) {
                if (positions == 0) return;

                debugger;
                var questions = parent.Questions();
                var startIndex = questions.indexOf(self);

                if (positions < 0) { // Item was moved up
                    questions[startIndex].SequenceNumber(questions[startIndex].SequenceNumber() + positions);
                    for (i = 1; i <= (positions * -1); i++) {
                        questions[startIndex - i].SequenceNumber(questions[startIndex - i].SequenceNumber() + 1);
                    }
                } else { // Item was moved down
                    questions[startIndex].SequenceNumber(questions[startIndex].SequenceNumber() + positions - 1);
                    for (i = 1; i < positions; i++) {
                        questions[startIndex + i].SequenceNumber(questions[startIndex + i].SequenceNumber() - 1);
                    }
                }
            };

            return self;
        }

        var versionModel = function (data) {
            var self = this;

            ko.mapping.fromJS(data, {
                'Questions': {
                    create: function (options) {
                        return new questionModel(options.data);
                    }
                }
            }, self);

            self.AddQuestion = function (parent) {
                // debugger;

                self.Questions.push(new questionModel({
                    Code: "",
                    LabelId: null,
                    UiProgCode: "",
                    Translation: "",
                    AnswerType: 2, // Text
                    Length: null,
                    Precision: null,
                    Scale: null,
                    SequenceNumber: null,
                    PreConditions: [],
                    Answers: [],
                    Mandatory: false,
                    Channels: []
                }));


                $("div.tabs").tabs();
                $("div.questionsList").accordion("refresh");


            };

            self.DeleteQuestion = function (question) {
                //  debugger;
                self.Questions.remove(question);
            };

            return self;
        }

        var questionSetModel = function (data) {
            var self = this;

            ko.mapping.fromJS(data, {
                'Versions': {
                    create: function (options) {
                        return new versionModel(options.data);
                    }
                }
            }, self);

            return self;
        };

        questionModel.prototype.toJSON = function () {
            var copy = ko.toJS(this); //easy way to get a clean copy
            delete copy.__ko_mapping__; //remove an extra property
            delete copy.DisplayScalePrecision;
            delete copy.DisplayLength;
            delete copy.DisplayAnswers;
            return copy; //return the copy to be serialized
        };

        versionModel.prototype.toJSON = function () {
            var copy = ko.toJS(this); //easy way to get a clean copy
            delete copy.__ko_mapping__; //remove an extra property
            return copy; //return the copy to be serialized
        };

        questionSetModel.prototype.toJSON = function () {
            var copy = ko.toJS(this); //easy way to get a clean copy
            delete copy.__ko_mapping__; //remove an extra property
            return copy; //return the copy to be serialized
        };

        var koModel = ko.mapping.fromJS(model, {
            'QuestionSet': {
                create: function (options) {
                    return new questionSetModel(options.data);
                }
            }
        });
        $.extend(questionSetViewModel, koModel);
        debugger;
        ko.applyBindings(questionSetViewModel);


    }
    return {
        init: init
    };
}(jQuery, ko));