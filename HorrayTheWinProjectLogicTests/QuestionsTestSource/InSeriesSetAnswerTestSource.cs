using System;
using System.Collections;
using Telegram.Bot.Types;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Data;
using HoorayTheWinProjectLogic;

namespace HorrayTheWinProjectLogicTests.QuestionsTestSource
{
	public class InSeriesSetAnswerTestSource : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			Update update = new Update();
			TestToBot testToBot = TestToBot.GetInstance();
			List<string> answers;
			testToBot.Manager.AnswerBase.TryGetValue(update.CallbackQuery!.Message!.Chat.Id, out answers!);
			if (answers.Count == testToBot.Manager.Test.AbstractQuestions.Count())
			{
				yield return new object[] { update.CallbackQuery!.Data = "a", Enums.BehaviorOptions.lastQuestion };
			}
			yield return new object[] { update.CallbackQuery!.Data = "a", Enums.BehaviorOptions.refreshKeybord };
			yield return new object[] { update.CallbackQuery!.Data = "Done", Enums.BehaviorOptions.nextQuestion };
			yield return new object[] { update.Message!.Text = "a", Enums.BehaviorOptions.invalidAnswer };
		}
	}
}
