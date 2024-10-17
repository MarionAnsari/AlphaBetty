using UnityEngine;

public class AlphabetTile : MonoBehaviour
{
    public GameObject alphabetTilePrefab; // Prefab for alphabet tiles
    private char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    // Percentage distribution for each letter (you can adjust this)
    private float[] letterPercentages = new float[26]
    {
        0.082f, 0.015f, 0.028f, 0.043f, 0.127f, 0.022f, 0.02f, 0.061f, 0.07f, 0.002f, 0.008f, 0.04f, 0.024f, 0.067f,
        0.075f, 0.019f, 0.001f, 0.057f, 0.063f, 0.091f, 0.028f, 0.01f, 0.024f, 0.002f, 0.02f, 0.001f
    };

    public void Initialize(bool[,] activeCells, float tileSize, int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (activeCells[i, j])
                {
                    char letter = GetRandomLetter();
                    GameObject letterTile = Instantiate(alphabetTilePrefab, transform);
                    letterTile.GetComponent<TextMesh>().text = letter.ToString(); // Assuming you have a TextMesh for displaying letters
                    
                    // Set the position above the grid
                    float xPos = (j * tileSize) - ((float)rows / 2 - tileSize / 2);
                    float yPos = (-i * tileSize) + ((float)columns / 2 - tileSize / 2) + 1; // Offset above the tile
                    letterTile.transform.localPosition = new Vector3(xPos, yPos, 0);
                    
                    // You may want to move the letter tiles to their final position here
                    // (Use a coroutine or animation to move them down to their grid position)
                }
            }
        }
    }

    private char GetRandomLetter()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulative = 0f;

        for (int i = 0; i < letters.Length; i++)
        {
            cumulative += letterPercentages[i];
            if (randomValue <= cumulative)
            {
                return letters[i];
            }
        }
        return 'A'; // Fallback
    }
}
