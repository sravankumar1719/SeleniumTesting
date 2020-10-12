Feature: Perform Multiple Searches

@single
Scenario: Performing Single Searches
	Given Navigate to Google home page
	When Enter search query <search> in the search box
	And Click on search button
	Then the search results should contain the search query <search>

Examples: 
| search   |
| Testing  |

@multiple
Scenario: Performing Multiple Searches
	Given Navigate to Google home page
	When Enter search query <search> in the search box
	And Click on search button
	Then the search results should contain the search query <search>

Examples: 
| search   |
| Testing  |
| Selenium |
| SpecFlow |