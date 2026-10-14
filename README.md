# CGTeam2 · 3D 방탈출 게임
> 제한 시간 안에 공간을 탐색하고, 아이템과 퍼즐을 이용해 탈출을 시도하는 Unity 3D 팀 프로젝트.

**[Unity 프로젝트 보기](https://github.com/snsn8105/CGTeam2/tree/main/Team2%20Project)** · **[핵심 상호작용 코드](https://github.com/snsn8105/CGTeam2/blob/main/Team2%20Project/Assets/ActionController.cs)**

## 프로젝트 소개
3D 공간에서 단서를 찾고 아이템을 수집하며 잠긴 구역과 탈출 장치를 해결하는 게임입니다. 플레이어 이동, 상호작용 안내, 인벤토리, 비밀번호 퍼즐과 성공·실패 씬을 연결했습니다.

| 항목 | 내용 |
| --- | --- |
| 장르 | 3D 탐색 · 방탈출 |
| 개발 환경 | Unity 2021.3.30f1, C# |
| 주요 기술 | CharacterController, Physics.SphereCast, Unity UI · TextMeshPro, SceneManager |
| 프로젝트 형태 | 팀 프로젝트 |
| 개인 담당 역할 | 탈출 로직 설계 및 구현 |

## 담당 역할
탈출 로직을 설계·구현하며, 공간을 탐색하고 장치를 조작하는 과정이 탈출 결과로 이어지도록 작업했습니다. 아래 항목은 팀 프로젝트에 포함된 전체 기능을 함께 소개합니다.

## 플레이 흐름
1. 공간을 이동하며 단서와 상호작용 가능한 오브젝트를 찾습니다.
2. 아이템을 수집하고 인벤토리에서 확인합니다.
3. 비밀번호 퍼즐과 문·장치 상호작용으로 탐색을 이어갑니다.
4. 에너지 셀 및 발사 장치와 상호작용해 탈출을 시도합니다.
5. 제한 시간 내 탈출하면 성공 씬으로, 시간이 끝나면 실패 씬으로 이동합니다.

## 주요 구현

### 오브젝트 탐색과 아이템 획득
`ActionController`는 SphereCast를 이용해 상호작용 대상을 탐색하고 UI 안내를 표시합니다. 획득 입력을 받으면 아이템 정보를 인벤토리에 전달한 뒤 월드의 오브젝트를 제거합니다.

인벤토리는 슬롯을 관리하며, 동일한 비장비 아이템은 이름을 기준으로 수량을 누적합니다.

**관련 코드:** [ActionController.cs](Team2%20Project/Assets/ActionController.cs) · [Inventory.cs](Team2%20Project/Assets/Inventory.cs)

### 비밀번호 퍼즐과 문
키패드의 입력 문자열을 관리하고 정답을 비교합니다. 정답일 때 문을 열고, 오답일 때 일정 시간 뒤 입력을 초기화합니다. 구역 이동용 문은 플레이어의 접근 여부와 상호작용 입력을 확인한 뒤 다음 씬으로 연결합니다.

**관련 코드:** [Keypad.cs](Team2%20Project/Assets/Keypad/Scripts/Keypad.cs) · [DoorInteraction.cs](Team2%20Project/Assets/DoorInteraction.cs) · [DoorOpen.cs](Team2%20Project/Assets/DoorOpen.cs)

### 탈출 장치와 결과 처리
에너지 셀 장치는 플레이어의 거리와 입력을 확인해 활성화되며, 발광·파티클·안내 문구로 상태 변화를 보여줍니다. 발사 버튼은 에너지 셀의 활성 상태와 플레이어 거리를 확인한 뒤 성공 씬으로 연결합니다.

별도의 타이머는 기본 15분에서 남은 시간을 표시하고, 시간이 소진되면 실패 씬으로 이동합니다.

**관련 코드:** [EnergyCellCombiner.cs](Team2%20Project/Assets/EnergyCellCombiner.cs) · [LaunchButton.cs](Team2%20Project/Assets/LaunchButton.cs) · [TimeController.cs](Team2%20Project/Assets/Scripts/TimeController.cs)

## 조작 방법
상호작용 대상에 따라 화면의 안내를 확인합니다.

| 입력 | 기능 |
| --- | --- |
| W / A / S / D | 이동 |
| Left Shift | 달리기 |
| E | 아이템 획득 및 장치·문 상호작용 |
| I | 인벤토리 열기·닫기 |
| F | 아이템 안내 닫기 |

## 실행 방법
1. 저장소를 내려받습니다.
2. Unity Hub에서 저장소 내부의 **`Team2 Project` 폴더**를 프로젝트로 추가합니다.
3. **Unity 2021.3.30f1**로 열고 패키지와 에셋 임포트가 끝날 때까지 기다립니다.
4. Build Settings에 등록된 `Title` 씬을 열어 실행합니다.

저장소 루트가 아닌 `Team2 Project`가 Unity 프로젝트 루트입니다.

## 구현을 살펴볼 지점
- 3D 카메라와 캐릭터 배치에 맞춘 상호작용 대상 탐색
- 아이템 획득 → 슬롯 갱신 → 안내 UI로 이어지는 처리
- 장치의 상태 변화와 탈출 성공 조건 연결
- 제한 시간과 씬 전환을 이용한 플레이 종료 처리

프로젝트에 포함된 캐릭터·환경·셰이더 등 외부 에셋의 권리는 각 제작자에게 있습니다.

