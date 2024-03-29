using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] Transform arrow = default;
    [SerializeField] 
    int distance;

    // 자신의 이웃 타일 및 다음경로 담아두기
    MapTile east, west, north, south, nextOnPath;

    // bool 값은 0과 1로도 표현 가능
    // 즉 무한대의 거리가 아니라면 경로가 존재한다 = true
    public bool isPath => distance != int.MaxValue;
    public bool isAlternative { get; set; }
    public bool isRail { get; set; }

    TileContent content;
    public TileContent Content
    {
        get => content;
        set
        {
            Debug.Assert(value != null, "Null assigned to content!");

            if (content != null) { content.Recycle(); }

            content = value;
            content.transform.localPosition = transform.localPosition;
        }
    }

    public MapTile NextOnPath => nextOnPath;

    // 적들이 가장자리를 따라 이동하게 하기위해 계산되는 값
    public Vector3 ExitPoint { get; private set; }

    public Direction PathDirection { get; private set; }

    static Quaternion
        norhRotation = Quaternion.Euler(90f, 0f, 0f),
        southRotation = Quaternion.Euler(90f, 180f, 0f),
        eastRotation = Quaternion.Euler(90f, 90f, 0f),
        westRotation = Quaternion.Euler(90f, 270f, 0f);

    MapTile GrowPathTo(MapTile neighbor, Direction direction)
    {
        Debug.Assert(isPath, "No Neighbor!");

        // 경로가 없거나, 전달받은 이웃이 없거나, 이웃의 경로가 없다면 null 반환
        if (!isPath || neighbor == null || neighbor.isPath) { return null; }

        // 목적지에서부터 거리 갱신, (최종목적지는 distance == 0, 그 바로 옆칸은 + 1)
        neighbor.distance = distance + 1;

        // 이웃의 경로는 나 자신
        neighbor.nextOnPath = this;

        // 이웃 타일의 가장자리 찾아내기
        neighbor.ExitPoint = neighbor.transform.localPosition + direction.GetHalfVector();

        neighbor.PathDirection = direction;

        // 이웃의 타입이 벽이 아니라면 이웃을 반환, 벽이라면 null 반환
        return neighbor.Content.Type != TileType.Wall ? neighbor : null;
    }

    MapTile GrowPathToTrain(MapTile neighbor, Direction direction)
    {
        Debug.Assert(isPath, "No Neighbor!");

        if(content.Type != TileType.Rail) { return null; }

        // 경로가 없거나, 전달받은 이웃이 없거나, 이웃의 경로가 없다면 null 반환
        if (!isPath || neighbor == null || neighbor.isPath) { return null; }

        // 이웃 타일의 가장자리 찾아내기
        neighbor.ExitPoint = neighbor.transform.localPosition + direction.GetHalfVector();

        neighbor.PathDirection = direction;

        // 이웃의 타입이 벽이 아니라면 이웃을 반환, 벽이라면 null 반환
        return neighbor.Content.Type == TileType.Rail ? neighbor : null;
    }

    #region PublicResponsiblity

    // { 서로 대칭관계의 방향 이웃 정의해주기
    public static void MakeEastWestNeighbors(MapTile east, MapTile west)
    {
        // *Debug.Assert(bool, string) = 첫번째 불값이 false면 띄우는 Log
        // 개발빌드에서만 동작하며 릴리즈 빌드에서는 동작을 안한다.

        Debug.Assert(west.east == null && east.west == null, "Redefined Neighbors!");
        west.east = east;
        east.west = west;
    }

    public static void MakeNorthSouthNeighbors(MapTile north, MapTile south)
    {
        Debug.Assert(north.south == null && south.north == null, "Redefined Neighbors!");
        south.north = north;
        north.south = south;
    }
    // } 서로 대칭관계의 방향 이웃 정의해주기

    // 경로를 찾기전 아직 대상타일이 존재하지 않는다고 정의하는 메서드 (distance == 무한대의 거리)
    public void ClearPath()
    {
        distance = int.MaxValue;
        nextOnPath = null;
    }
    
    // 최종 목적지 정의 메서드, 도착했으니 다음 타일은 null
    public void BecomDestination()
    {
        distance = 0;
        nextOnPath = null;
        ExitPoint = transform.localPosition;
    }

    public MapTile GrowPathNorth() => GrowPathTo(north, Direction.South);
    public MapTile GrowPathSouth() => GrowPathTo(south, Direction.North);
    public MapTile GrowPathEast() => GrowPathTo(east, Direction.West);
    public MapTile GrowPathWest() => GrowPathTo(west, Direction.East); 


    // 화살표 타일이 어느방향을 바라봐야 하는지 정의해주는 메서드
    // TODO : 해당 메서드를 이용해서 나중에 레일 회전방향 잡아주면 될 듯
    public void ShowPath()
    {
        if(distance == 0)
        {
            arrow.gameObject.SetActive(false);
            return;
        }

        arrow.gameObject.SetActive(true);

        // MapTile이 참조하고있는 이웃의 정보와 비교하여 다음 타일이 어느 방향인지 찾기
        arrow.localRotation =
            nextOnPath == north ? norhRotation :
            nextOnPath == east ? eastRotation :
            nextOnPath == south ? southRotation :
            westRotation;
    }

    public void HidePath()
    {
        arrow.gameObject.SetActive(false);
    }

    public MapTile GetRailTile()
    {
        if(north != null)
        {
            if(north.content.Type == TileType.Rail) { return north; }
        }
        else if (east != null)
        {
            if(east.content.Type == TileType.Rail) {  return east; }
        }
        else if (south != null)
        {
            if(south.content.Type == TileType.Rail) {  return south; }
        }
        else if (west != null)
        {
            if(west.content.Type == TileType.Rail) {  return west; }
        }

        return null;
    }

    #endregion
}
