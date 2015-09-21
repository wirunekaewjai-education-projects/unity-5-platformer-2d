# unity-5-platformer-2d
Platformer 2D Game (Unity 5) for Video Tutorials

### Update #6 :: 2015.09.21 - 03.05 PM
- เปลี่ยน Script "Gate" เป็น "Gate FSM"
- เพิ่ม UserInterface แสดง Coin ใน Play Scene
- ตั้งกฏการเปิด Gate ดูนับจาก Coin
- สร้างระบบจัดการ Coin เป็นแบบ Singleton + Observable (ใช้ Delegate ในการ Subscribe)

### Update #5 :: 2015.09.20 - 10.57 PM
- ปรับโค้ด Finite State Machine ทั้ง Player และ Bot
- แก้บั๊ค Player เวลายืนบนแท่นเลื่อน
- เพิ่ม Menu Scene

### Update #4 :: 2015.09.19 - 11.40 PM
- เพิ่มโค้ดตัวอย่างที่เป็น Finite State Machine

### Update #3 :: 2015.09.19 - 01:27 AM
- เพิ่ม Platform แบบเลื่อนไปเลื่อนมา (Tween)
- เพิ่มอีเว้นท์การตายของผู้เล่นและบอทดังนี้
--- ถ้าบอทถูกผู้เล่นกระโดดเหยียบหัวจะตาย
--- ถ้าผู้เล่นถูกบอทเดินชนจะตาย (จะรีสตาร์ทซีนภายใน 3 วิ)

### Update #2 :: 2015.09.18 - 11:56 PM
- เพิ่มเหรียญ (ยังไม่ได้ใส่อีเว้นท์เวลาชน)
- สร้างบอทโง่ๆ เดินไปเดินมา (ยังไม่ได้ใส่อีเว้นท์เวลาชน)
- สร้างประตูบอส (เวลาเดินไปใกล้ๆ แล้วประตูเปิดอัตโนมัติ)

### Initialize
- ตัวละครยืน, วิ่ง, กระโดด และปีนบันได
- มี Platform ประเภท Normal, Jump-Through และ Ladder
- Camera Follow
