# UnderveilExampleMod

[Underveil Mod Loader](https://github.com/DevP0tion/underveil-mod-loader)용 예시 모드입니다. 새 모드를 만들 때 복사해서 시작할 수 있습니다.

비공식 팬 프로젝트로, 게임 개발사와 관련이 없습니다. 이 저장소에는 게임 파일이 들어 있지 않습니다.

## 내용

- `mod.json`: 모드 정보
- `UnderveilExampleMod.cs`: `IMod` 진입점, 로그 출력, `GameManager.Start` Harmony 패치
- `Events/ExampleEvent.cs`: 이벤트 풀에 새 이벤트 "Forgotten Shrine" 추가

## 빌드

Underveil Mod Loader가 설치된 게임과 .NET SDK가 필요합니다. 참조 어셈블리를 이 게임 폴더에서 가져옵니다.

```bash
dotnet build -c Release -p:GameDir="C:\path\to\Underveil Demo"
```

빌드하면 DLL과 `mod.json`이 게임 폴더의 `Mods/UnderveilExampleMod`로 복사됩니다. 게임을 끄고 빌드한 뒤 다시 실행하면 모드 창에 나타납니다.

## 라이선스

[The Unlicense](LICENSE)
