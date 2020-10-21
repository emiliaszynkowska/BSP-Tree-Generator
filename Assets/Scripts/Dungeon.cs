using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dungeon
    {
        public Dungeon left;
        public Dungeon right;
        public Rect rect;
        public Rect room;

        public Dungeon(Rect rect)
        {
            this.rect = rect;
        }

        public bool IsLeaf()
        {
            return left == null && right == null; 
        }

        public bool Split(int minRoomSize, int maxRoomSize)
        {
            if (!IsLeaf())
                return false;
            
            bool splitH;
            if (rect.width / rect.height >= 1.25)
                splitH = false;
            else if (rect.height / rect.width >= 1.25)
                splitH = true;
            else
                splitH = Random.Range(0, 1) > 0.5;

            if (Math.Min(rect.height, rect.width) / 2 < minRoomSize)
                return false;

            if (splitH)
            {
                int split = Random.Range(minRoomSize, (int) (rect.width - minRoomSize));
                left = new Dungeon(new Rect(rect.x, rect.y, rect.width, split));
                right = new Dungeon(new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
            }
            else
            {
                int split = Random.Range(minRoomSize, (int) (rect.height - minRoomSize));
                left = new Dungeon(new Rect (rect.x, rect.y, split, rect.height));
                right = new Dungeon(new Rect (rect.x + split, rect.y, rect.width - split, rect.height));
            }

            return true;
        }

        public void CreateRoom()
        {
            if (left != null)
                left.CreateRoom();
            if (right != null)
                right.CreateRoom();
            if (IsLeaf())
            {
                int width = (int) Random.Range(rect.width / 2, rect.width - 2);
                int height = (int) Random.Range(rect.height / 2, rect.height - 2);
                int roomX = (int) Random.Range(1, rect.width - width - 1);
                int roomY = (int) Random.Range(1, rect.height - height - 1);
                
                room = new Rect(rect.x + roomX, rect.y + roomY, width, height);
            }
        }

    }
