# Calculate-EMA
Share Price Monitoring Script in C#

It is for a basic share or stock market monitoring application.

Folder Structure (image attached)
• Original Folder = Original Data
• Adjusted Folder = Adjusted Data is saved
• Summary File = Result summary of every CSV
• Position File = New Result from code (True Results and changes)
• xPosition File = the previous results (to compare against)

Steps:
1. Loop through every CSV in the folder “Original”

2. Clean the Data
a. Remove 0 Volume lines
b. Remove blanks
c. Delete duplicate dates
d. Delete weekends (Saturdays and Sundays)

3. Calculate a simple daily mid weight EMA
a. Mid weight = (o+h+l+c)/4
b. EMA of the mid weight with N = 3, however value to be available in code so I can change if needed

4. Calculate a monthly mid weighted EMA
a. Find the H, L, O, C of the month
b. Monthly Mid Weight
c. EMA of the mid weight with N = 3, however value to be available in code so I can change if needed

5. Saving:
a. Save the file into “adjusted” folder with the Daily and Monthly columns added
b. Test if DailyEMA>MonthlyEMA and add the latest date line into the summary file
i. Eg. Column headers [Security Code, date, close, ema daily, ema monthly, True/False]

6. Final Step
a. Create a new csv called “Positions”, with headers [Security Code, position]
b. If the True is new (was not in “xPositions”, i.e. the previous result from running the code) – then [Security Code, ‘New’]
c. If the true was present in the previous result [Security Codename, ‘Current’]
d. If xPosition has a line which was not in the new results [Security Code, ‘Sold’]



