# SIRIUS-Demo-2D-Platformer

## 📌 개요

이 프로젝트는 Unity 2D 플랫포머 게임에서 사용되는 핵심 스크립트 모음입니다.
플레이어 이동, 점프, 카메라 추적, 배경 패럴럭스 효과 등을 구현하며,
각각의 스크립트는 다음과 같은 역할을 수행합니다.

---

## 📜 스크립트 설명

### 1. **PlatformerCharacter2D.cs**&#x20;

* **역할**: 플레이어 캐릭터의 이동과 점프, 애니메이션 동작을 제어하는 핵심 스크립트
* **주요 기능**:

  * `Move()`를 통해 **좌우 이동, 웅크리기, 점프** 처리
  * Rigidbody2D를 이용한 물리 기반 이동
  * GroundCheck, CeilingCheck를 사용해 **지면 및 천장 충돌 감지**
  * 애니메이터(`Animator`) 파라미터(`Ground`, `Speed`, `vSpeed`, `Crouch`) 제어
  * `Flip()`으로 좌우 캐릭터 반전
* **특징**:

  * 공중 제어 여부(`m_AirControl`) 설정 가능
  * 웅크릴 때 속도 감소(`m_CrouchSpeed`)
  * **기본 이동 시스템의 핵심**

---

### 2. **Platformer2DUserControl.cs**&#x20;

* **역할**: **플레이어의 입력을 받아 PlatformerCharacter2D에 전달**하는 사용자 입력 컨트롤러
* **주요 기능**:

  * 키보드 입력(`Horizontal`, `Jump`, `LeftControl`)을 감지
  * FixedUpdate에서 물리 기반 이동값 전달 (`m_Character.Move()`)
  * Update에서 **점프 입력은 한 프레임만 감지**하여 놓침 방지
* **특징**:

  * PC 전용 입력(`Input`) 사용 (모바일 CrossPlatformInput 제거된 버전)
  * `RequireComponent` 속성으로 PlatformerCharacter2D와 함께 사용 보장

---

### 3. **Camera2DFollow\.cs**&#x20;

* **역할**: 플레이어를 따라가는 **동적 카메라**
* **주요 기능**:

  * 플레이어의 이동 속도 변화에 따라 **Look Ahead(미리 보기)** 연출
  * `SmoothDamp()`로 자연스러운 카메라 이동
  * 이동 방향에 따라 화면이 약간 앞으로 움직여 **시야 확보** 효과
* **추천 상황**: 빠른 이동, 방향 전환이 많은 플랫포머 게임에 적합

---

### 4. **CameraFollow\.cs**&#x20;

* **역할**: 플레이어를 따라가되, **여유 공간(Margin)을 둔 부드러운 카메라**
* **주요 기능**:

  * 플레이어가 `xMargin`, `yMargin` 이상 벗어나면 카메라가 따라옴
  * `Lerp()`를 사용한 부드러운 추적
  * `minXAndY`, `maxXAndY`로 카메라 이동 범위 제한
* **추천 상황**: 비교적 안정적이고 화면이 흔들리지 않는 연출이 필요한 게임

---

### 5. **Parallax\_Curtis.cs**&#x20;

* **역할**: **배경 패럴럭스 효과** 구현
* **주요 기능**:

  * 카메라 이동에 따라 배경이 깊이에 비례해 다른 속도로 움직임
  * `backgrounds[i].position.z`를 기반으로 패럴럭스 비율 자동 계산(`maxDistance`로 정규화)
* **특징**:

  * 가까운 배경일수록 많이 움직이고, 먼 배경일수록 적게 움직임
  * 단순한 패럴럭스 구현에 적합

---

## 🔧 추천 세팅

### PlatformerCharacter2D

* `m_MaxSpeed`: 8 \~ 10
* `m_JumpForce`: 300 \~ 400
* `m_CrouchSpeed`: 0.3 \~ 0.5

### CameraFollow

* `xMargin = 1.5f`, `yMargin = 1f`
* `xSmooth = 6 ~ 8`, `ySmooth = 6 ~ 8`
* `minXAndY` / `maxXAndY`: 맵 크기에 맞게 설정

### Parallax\_Curtis

* 배경 Z값: 가까운 배경은 작은 Z값(예: -1), 먼 배경은 큰 Z값(예: -10)

---

## 📂 사용 순서

1. **플레이어**: `PlatformerCharacter2D` + `Platformer2DUserControl`
2. **카메라**: `CameraFollow` 또는 `Camera2DFollow` 중 선택
3. **배경**: `Parallax_Curtis`를 배경 오브젝트에 추가

---
