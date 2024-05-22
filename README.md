# TeamONY-Game
 
## 日本語アセットのダウンロード

大きいフォントファイルなので以下のスクリプトを実行してフォントファイル、metaファイルをダウンロードしてください。

```sh
./download-assets.sh
```

## コミットメッセージのルール

・Add: 新しい機能
```sh
git commit -m "[Add]:********"
```

・Fix: バグの修正
```sh
git commit -m "[Fix]:********"
```

・Docs: ドキュメントのみ変更
```sh
git commit -m "[Docs]:********"
```

・Style: 空白、フォーマット、セミコロン追加
```sh
git commit -m "[Style]:********"
```

・Refac: 仕様に影響がないコード改善(リファクタ) 
```sh
git commit -m "[Refac]:********"
<<<<<<< HEAD
```

## コミットする際の注意点

・OpenAIのAPIキーをコミットしないように注意してください。
・コミット粒度は細かくすることを推奨します。