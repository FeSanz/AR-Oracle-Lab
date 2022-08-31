using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.SimpleSlider.Scripts
{
	/// <summary>
	/// Realiza funciones de centrar/enfocar sobre el child  y deslizar.
	/// </summary>
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollSnapBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField, Tooltip("Contenedor de páginas")] private GridLayoutGroup GridLayout;
		[SerializeField, Tooltip("Indicadores de págiona")] private GameObject Pagination;
		[SerializeField, Tooltip("Umbral de deslizamiento")] private int SwipeThreshold = 50;
		[SerializeField, Tooltip("Tiempo de deslizamiento")] private float SwipeTime = 0.5f;

		[SerializeField, Tooltip("Pagina con video")] private GameObject[] video;

		private ScrollRect _scrollRect;
		private Toggle[] _pageToggles;

		private bool _drag;
		private bool _lerp;
		private int _page;
		private float _dragTime;

		/// <summary>
		/// Se llama al habilitar el script o Gameobjet antes de cualquier Update
		/// Se llama una vez durante la vida útil del script
		/// </summary>
		void Start()
		{
			Screen.orientation = ScreenOrientation.Portrait; 
			
			_scrollRect = GetComponent<ScrollRect>();
			_scrollRect.horizontalNormalizedPosition = 0;
			_pageToggles = Pagination.GetComponentsInChildren<Toggle>(true);
			
			gameObject.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
			
			UpdatePaginator(_page);
			enabled = true;
		}
		
		public void Update()
		{
			// Redimensiona tamaño de celda de acuerdo al tamaño (RectTransform) del padre
			GridLayout.cellSize = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
			
			if (!_lerp || _drag) return;

			if (Pagination)
			{
				var page = GetCurrentPage();

				if (!_pageToggles[page].isOn)
				{
					UpdatePaginator(page);
					VideoSlide(page);
				}
			}

			var horizontalNormalizedPosition = (float) _page / (_scrollRect.content.childCount - 1);

			_scrollRect.horizontalNormalizedPosition = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, horizontalNormalizedPosition, 5 * Time.deltaTime);

			if (Math.Abs(_scrollRect.horizontalNormalizedPosition - horizontalNormalizedPosition) < 0.001f)
			{
				_scrollRect.horizontalNormalizedPosition = horizontalNormalizedPosition;
				_lerp = false;
			}
		}

		private void VideoSlide(int page)
		{
			foreach (GameObject item in video)
			{
				item.SetActive(false);
			}

			if (page < video.Length)
			{
				video[page].SetActive(true);
			}
		}

		/// <summary>
		/// Inicia Scroll Rect - (Random o Normal)
		/// </summary>
		/// <param name="random"></param>
		public void Initialize(bool random = false)
		{
			_scrollRect.horizontalNormalizedPosition = 0;
			_pageToggles = Pagination.GetComponentsInChildren<Toggle>(true);
			
			if (random)
			{
				ShowRandom();
			}

			UpdatePaginator(_page);
			enabled = true;
		}

		/// <summary>
		/// Método para iniciar con una página aleatoria
		/// </summary>
		public void ShowRandom()
		{
			if (_scrollRect.content.childCount <= 1) return;

			int page;

			do
			{
				page = UnityEngine.Random.Range(0, _scrollRect.content.childCount);
			}
			while (page == _page);

			_lerp = false;
			_page = page;
			_scrollRect.horizontalNormalizedPosition = (float) _page / (_scrollRect.content.childCount - 1);
		}

		private void Slide(int direction)
		{
			direction = Math.Sign(direction);

			if (_page == 0 && direction == -1 || _page == _scrollRect.content.childCount - 1 && direction == 1) return;

			_lerp = true;
			_page += direction;
		}

		private int GetCurrentPage()
		{
			return Mathf.RoundToInt(_scrollRect.horizontalNormalizedPosition * (_scrollRect.content.childCount - 1));
		}

		private void UpdatePaginator(int page)
		{
			if (Pagination)
			{
				_pageToggles[page].isOn = true;
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_drag = true;
			_dragTime = Time.time;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var page = GetCurrentPage();

			if (page != _page)
			{
				_page = page;
				UpdatePaginator(page);
			}
		}
		
		public void OnEndDrag(PointerEventData eventData)
		{
			var delta = eventData.pressPosition.x - eventData.position.x;

			if (Mathf.Abs(delta) > SwipeThreshold && Time.time - _dragTime < SwipeTime)
			{
				var direction = Math.Sign(delta);

				Slide(direction);
			}

			_drag = false;
			_lerp = true;
		}
	}
}