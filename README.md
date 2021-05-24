# rpg-project
首先，如果你需要詳細的有關此專案的紀錄，請看專題書面報告(pdf)

其中包含了為何我們製作此遊戲，與心路歷程及各項功能演示。

============================================
所需工具：Unity, VS code
繪圖軟體 : Paper53, tile map
資料庫 : 直接存取於本機
============================================


部分功能簡易示意圖

![1](https://user-images.githubusercontent.com/39086959/119364381-6ac6a980-bce1-11eb-8bc8-a7d15f49aa92.png)
![2](https://user-images.githubusercontent.com/39086959/119364388-6bf7d680-bce1-11eb-9a8e-f58388c7c1e3.png)



## Git與GitHub的使用(整理ing)
- Local端下載與建立與Github連結的專案

    cmd進入所要放置的資料夾
    git clone [下面任選一個網址]
    HTTPS : https://github.com/opiuclv/rpg-project.git
    SSH   : git@github.com:opiuclv/rpg-project.git

- 建立Git遠端branch [每台電腦都各別建立]

    git push origin master:[branch名稱]

- 建立完之後手動進入該分支

    git checkout [branch名稱]

- 如何更新github上自己的branch內容
  先進到master，把最新Github上的master合併到本地master

    git pull     ==會產生額外的commit==
    git pull --rebase     ==不會產生額外的commit==
    git fetch ==先更新到本機端再選擇要不要合併(merge)==

- 然後進到自己的branch    [後面的no-ff 是為了讓分支更清楚]
  輸入完下面指令會進到vim的畫面，此時請輸入:wq冒號也要打
  意思是write and quit

    git merge master --no-ff

- 有更新內容並測試ok後

    git status查看更新狀態
    git add . 加入所有更新
    git commit -m "更新內容" 
    git push上傳內容
    git log確認commit紀錄


- 確認沒問題後回到master

    git merge [branch名稱] --no-ff ==合併分支到主分支==
    之後一樣就add, commit, push上去
