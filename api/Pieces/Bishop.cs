﻿using api.models.api;
using api.pieces.interfaces;

namespace api.pieces
{
    public class Bishop : IPiece
    {
        public string Color { get; set; }

        public Bishop(string Color)
        {
            this.Color = Color;
        }

        public List<int[]> GetPaths(Board board, int[] coords, bool check)
        {
            List<int[]> moves = new();

            int col = coords[0];
            int row = coords[1];
            int[] colInc = { -1, -1, 1, 1 };
            int[] rowInc = { 1, -1, -1, 1 };
            Direction[] Directions =
            {
                Direction.FromTopRightToBottomLeft,
                Direction.FromTopLeftToBottomRight,
                Direction.FromTopRightToBottomLeft,
                Direction.FromTopLeftToBottomRight,
            };

            for (int i = 0; i < 4; i++, col = coords[0], row = coords[1])
            {
                if (
                    board.Rows[col].Squares[row].PinnedDirection != Direction.None
                    && board.Rows[col].Squares[row].PinnedDirection != Directions[i]
                )
                {
                    continue;
                }

                col += colInc[i];
                row += rowInc[i];

                while (col >= 0 && row >= 0 && col < 8 && row < 8)
                {
                    BoardSquare square = board.Rows[col].Squares[row];
                    if (square.Piece == null || square.Piece.Color != this.Color)
                    {
                        if (check)
                        {
                            if (square.CheckBlockingColor == this.Color)
                            {
                                moves.Add(new int[] { col, row });
                            }
                        }
                        else
                        {
                            moves.Add(new int[] { col, row });
                        }

                        if (square.Piece != null)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                    col += colInc[i];
                    row += rowInc[i];
                }
            }

            return moves;
        }

        public List<int[]> GetPressure(Board board, int[] coords)
        {
            List<int[]> moves = new();

            int col = coords[0];
            int row = coords[1];
            int[] colInc = { -1, -1, 1, 1 };
            int[] rowInc = { 1, -1, -1, 1 };

            for (int i = 0; i < 4; i++, col = coords[0], row = coords[1])
            {
                col += colInc[i];
                row += rowInc[i];

                while (col >= 0 && row >= 0 && col < 8 && row < 8)
                {
                    moves.Add(new int[] { col, row });

                    if (board.Rows[col].Squares[row].Piece != null)
                    {
                        if (board.Rows[col].Squares[row].Piece is King)
                        {
                            col += colInc[i];
                            row += rowInc[i];
                            if (col >= 0 && row >= 0 && col < 8 && row < 8)
                            {
                                moves.Add(new int[] { col, row });
                            }
                        }

                        break;
                    }

                    col += colInc[i];
                    row += rowInc[i];
                }
            }

            return moves;
        }

        public string ToString(bool pipeSeparated)
        {
            if (pipeSeparated)
            {
                return Color + "|Bishop";
            }

            return Color + "Bishop";
        }
    }
}