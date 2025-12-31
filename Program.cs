//*****************************************************************************
//** 1970. Last Day Where You Can Still Cross                       leetcode **
//*****************************************************************************
//** Across the grid where dry paths fade to blue,                           **
//** Each day steals land the traveler once knew.                            **
//** We search the last dawn feet may still descend,                         **
//** When earth yet links the start, the goal, the end.                      **
//*****************************************************************************

typedef struct
{
    int r;
    int c;
} Node;

bool canCross(int row, int col, int** cells, int day)
{
    int i = 0;

    int** grid = (int**)malloc(sizeof(int*) * row);
    for (i = 0; i < row; i++)
    {
        grid[i] = (int*)calloc(col, sizeof(int));
    }

    for (i = 0; i < day; i++)
    {
        int r = cells[i][0] - 1;
        int c = cells[i][1] - 1;
        grid[r][c] = 1;
    }

    bool** visited = (bool**)malloc(sizeof(bool*) * row);
    for (i = 0; i < row; i++)
    {
        visited[i] = (bool*)calloc(col, sizeof(bool));
    }

    Node* queue = (Node*)malloc(sizeof(Node) * row * col);
    int head = 0;
    int tail = 0;

    for (i = 0; i < col; i++)
    {
        if (grid[0][i] == 0)
        {
            queue[tail++] = (Node){0, i};
            visited[0][i] = true;
        }
    }

    int dirs[4][2] =
    {
        {-1, 0},
        { 1, 0},
        { 0,-1},
        { 0, 1}
    };

    while (head < tail)
    {
        Node u = queue[head++];

        if (u.r == row - 1)
        {
            for (i = 0; i < row; i++)
            {
                free(grid[i]);
                free(visited[i]);
            }
            free(grid);
            free(visited);
            free(queue);
            return true;
        }

        for (i = 0; i < 4; i++)
        {
            int nr = u.r + dirs[i][0];
            int nc = u.c + dirs[i][1];

            if (nr < 0 || nr >= row || nc < 0 || nc >= col)
            {
                continue;
            }

            if (grid[nr][nc] == 1 || visited[nr][nc])
            {
                continue;
            }

            visited[nr][nc] = true;
            queue[tail++] = (Node){nr, nc};
        }
    }

    for (i = 0; i < row; i++)
    {
        free(grid[i]);
        free(visited[i]);
    }

    free(grid);
    free(visited);
    free(queue);

    return false;
}

int latestDayToCross(int row, int col, int** cells, int cellsSize, int* cellsColSize)
{
    int left = 0;
    int right = cellsSize;
    int retval = 0;

    while (left <= right)
    {
        int mid = left + ((right - left) >> 1);

        if (canCross(row, col, cells, mid))
        {
            retval = mid;
            left = mid + 1;
        }
        else
        {
            right = mid - 1;
        }
    }

    return retval;
}