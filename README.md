# ENPEG
(Windows) Explorer Navigation Pane Entry Generator
<br>
When you don't want to use Quick Access anymore
# Info

Create new Navigation Entries like this one
 
<div><img src="https://i.imgur.com/Mrexbrk.png"></div>

# How to use

## Generate new Entry
1. Download the latest Release from GitHub <a href="https://github.com/Boring69/ENPEG/releases/">here</a>
2. Run the executable as Administrator
3. Type in a name for the Navigation Entry
4. Select the Destination Path and the Icon that's going to be displayed by clicking on the buttons
   - Icons preferably 32px x 32px
6. Click Generate

## Remove existing Entry
1. Open Regedit (Win + r → regedit.exe)
2. Navigate to following key: HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Desktop\NameSpace
3. You will find several alpha-numeric entries (e.g. {3306F6B9-D597-1EE3-9505-142C4CD639AC}). Click through them until you find your desired Entry. The name of every Navigation Entry is the default Value which is displayed at the very right of the registry view.
4. Delete the Entry

## Additional Notes
- I apologize for the horrendous UI
