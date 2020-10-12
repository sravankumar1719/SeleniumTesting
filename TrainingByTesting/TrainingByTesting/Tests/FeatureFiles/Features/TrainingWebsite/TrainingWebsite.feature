Feature: Training Website

Background: 
	Given Navigate to Training Website Home page
	And Change the Website language to specified language

@headernames
Scenario: Verify the Header names in Home page
	Then Verify the Header names in the home page of the website

@searchresults
Scenario: Verify the Search results for specific search items
	When I click on Search input field
	And I click on the country <Country>
	And Select the state <State>
	Then Verify the search result <Result>

Examples:
| Country    | State    | Result                      |
| Belarus    | Minsk    | Belarus                     |
| Belarus    | Gomel    | Belarus                     |
| Russia     | Ryazan   | Russia                      |
| Armenia    | Yerevan  | No trainings are available. |
| Uzbekistan | Tashkent | Uzbekistan                  |
| Poland     | Krakow   | No trainings are available. |
