using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggGenerator : MonoBehaviour
{
    public Creature mom;
    public Creature dad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GenerateEgg();
        }
    }

    void GenerateEgg()
    {
		int[] childGenes = GenerateChildGenes();
		PrintGenes(childGenes);
    }

    List<int> GenerateGenePool()
    {
        List<int> list = new List<int>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < mom.genes[i]; j++)
            {
                list.Add(i);
            }

            for (int j = 0; j < dad.genes[i]; j++)
            {
                list.Add(i);
            }
        }

        return list;
    }

    int[] GenerateChildGenes()
    {
		List<int> genePool = GenerateGenePool();

		int[] childGenes = {0, 0, 0, 0, 0, 0, 0, 0};

        int[] genePositions = { 8, 8, 8 }; //Save positions already chosen

        int currentPosition = 0; //Current position to save

        while (true)
        {
            int tempPosition = Random.Range(0, 6); //Random parent gene

			bool newGene = true;

			for(int i = 0; i < 3; i++)
			{
				if(genePositions[i] == tempPosition) //Check if parent gene was already picked
				{
					newGene = false;
					break;
				}
			}

			if(newGene)
			{
				genePositions[currentPosition] = tempPosition; //Register position to not choose it again
				currentPosition++;

				childGenes[genePool[tempPosition]]++; //Register gene in child
			}

			if(currentPosition > 2) //Exit while loop because three genes have been picked
			{
				return childGenes;
			}
        }
    }

	void PrintGenes(int[] genes)
	{
		string result = "";

		for(int i = 0; i < genes.Length; i++)
		{
			result += (genes[i]);
		}

		Debug.Log(result);
	}
}
