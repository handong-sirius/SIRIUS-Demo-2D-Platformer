using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character; // 캐릭터 이동을 담당하는 스크립트
        private bool m_Jump;                       // 점프 입력 여부

        private void Awake()
        {
            // 같은 오브젝트에 붙어있는 PlatformerCharacter2D를 가져오기
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            // Update에서 점프 입력을 감지 (한 프레임만 눌리는 입력을 놓치지 않기 위해)
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump"); // 기본 Input 사용 (Space 등)
            }
        }

        private void FixedUpdate()
        {
            // 웅크리기 입력 (왼쪽 Ctrl 키)
            bool crouch = Input.GetKey(KeyCode.LeftControl);

            // 좌우 이동 입력 (-1 ~ 1)
            float h = Input.GetAxis("Horizontal");

            // 캐릭터 이동 함수로 입력 전달
            m_Character.Move(h, crouch, m_Jump);

            // 점프 초기화 (다음 FixedUpdate에서 중복 점프 방지)
            m_Jump = false;
        }
    }
}
