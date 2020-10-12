Feature: Perform Search

Scenario: Performing Google Search
	Given Launch Chrome browser
	When Enter the query Selenium in the search box
	And Click on Search Button
	Then the search results should contain the search query Selenium