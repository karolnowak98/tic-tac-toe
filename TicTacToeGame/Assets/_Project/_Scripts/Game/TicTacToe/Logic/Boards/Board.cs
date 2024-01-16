using System.Collections.Generic;
using UnityEngine;
using GlassyCode.TTT.Game.TicTacToe.Data.Enums;

namespace GlassyCode.TTT.Game.TicTacToe.Logic.Boards
{
    public readonly struct Board : IBoard
    {
        private readonly Symbol[,] _fields;

        public Board(Vector2Int fieldSizes)
        {
            _fields = new Symbol[fieldSizes.x, fieldSizes.y];
        }

        public void Reset()
        {
            for (var x = 0; x < _fields.GetLength(0); x++)
            {
                for (var y = 0; y < _fields.GetLength(1); y++)
                {
                    _fields[x, y] = Symbol.None;
                }
            }
        }

        public Vector2Int? GetRandomEmptyField()
        {
            var emptyFields = new List<Vector2Int>();

            for (var x = 0; x < _fields.GetLength(0); x++)
            {
                for (var y = 0; y < _fields.GetLength(1); y++)
                {
                    if (_fields[x, y] == Symbol.None)
                    {
                        emptyFields.Add(new Vector2Int(x, y));
                    }
                }
            }

            return emptyFields.Count > 0 ? emptyFields[Random.Range(0, emptyFields.Count)] : (Vector2Int?) null;
        }
        
        public Vector2Int[] GetWinningCoords(Symbol symbol)
        {
            for (var i = 0; i < _fields.GetLength(0); i++)
            {
                if (_fields[i, 0] == symbol && _fields[i, 1] == symbol && _fields[i, 2] == symbol)
                    return new []{new Vector2Int(i, 0), new Vector2Int(i, 1), new Vector2Int(i, 2)};

                if (_fields[0, i] == symbol && _fields[1, i] == symbol && _fields[2, i] == symbol)
                    return new []{new Vector2Int(0, i), new Vector2Int(1, i), new Vector2Int(2, i)};
            }

            if (_fields[0, 0] == symbol && _fields[1, 1] == symbol && _fields[2, 2] == symbol)
                return new []{new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(2, 2)};

            if (_fields[0, 2] == symbol && _fields[1, 1] == symbol && _fields[2, 0] == symbol)
                return new []{new Vector2Int(0, 2), new Vector2Int(1, 1), new Vector2Int(2, 0)};

            return null;
        }

        public Symbol CheckWinForPlayers()
        {
            if (CheckWinForSymbol(Symbol.X))
                return Symbol.X;

            if (CheckWinForSymbol(Symbol.O))
                return Symbol.O;

            return Symbol.None;
        }

        public void SetSymbolAtPosition(Vector2Int fieldPos, Symbol symbol)
        {
            if (IsPositionValid(fieldPos) && !IsFieldOccupiedAtPosition(fieldPos))
            {
                _fields[fieldPos.x, fieldPos.y] = symbol;
            }
        }

        public void MarkAsEmptyAtPosition(Vector2Int fieldPos)
        {
            if (IsPositionValid(fieldPos))
            {
                _fields[fieldPos.x, fieldPos.y] = Symbol.None;
            }
        }

        public bool IsFieldOccupiedAtPosition(Vector2Int fieldPos)
        {
            return IsPositionValid(fieldPos) && _fields[fieldPos.x, fieldPos.y] != Symbol.None;
        }

        public Symbol GetSymbolAtPosition(Vector2Int fieldPos)
        {
            return IsPositionValid(fieldPos) ? _fields[fieldPos.x, fieldPos.y] : Symbol.None;
        }

        public bool IsFull()
        {
            for (var x = 0; x < _fields.GetLength(0); x++)
            {
                for (var y = 0; y < _fields.GetLength(1); y++)
                {
                    if (_fields[x, y] == Symbol.None)
                        return false;
                }
            }

            return true;
        }

        private bool CheckWinForSymbol(Symbol symbol)
        {
            for (var i = 0; i < _fields.GetLength(0); i++)
            {
                if (_fields[i, 0] == symbol && _fields[i, 1] == symbol && _fields[i, 2] == symbol)
                    return true;

                if (_fields[0, i] == symbol && _fields[1, i] == symbol && _fields[2, i] == symbol)
                    return true;
            }

            if (_fields[0, 0] == symbol && _fields[1, 1] == symbol && _fields[2, 2] == symbol)
                return true;

            if (_fields[0, 2] == symbol && _fields[1, 1] == symbol && _fields[2, 0] == symbol)
                return true;

            return false;
        }
        
        //Might be an over-engineering example, but we have assurance that we won't meet IndexOutOfRangeException
        private bool IsPositionValid(Vector2Int fieldPos)
        {
            return fieldPos.x >= 0 && fieldPos.x < _fields.GetLength(0) && fieldPos.y >= 0 && fieldPos.y < _fields.GetLength(1);
        }
    }
}